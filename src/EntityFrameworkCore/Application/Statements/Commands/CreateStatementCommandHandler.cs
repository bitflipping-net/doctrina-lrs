using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Personas.Commands;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Relations;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand, Guid>
    {
        private readonly IStoreDbContext _dbContext;
        private readonly IClientHttpContext _clientHttpContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStoreHttpContext _storeContext;

        public CreateStatementCommandHandler(IStoreDbContext dbContext, IClientHttpContext clientHttpContext, IMediator mediator, IMapper mapper, IStoreHttpContext storeContext)
        {
            _dbContext = dbContext;
            _clientHttpContext = clientHttpContext;
            _mediator = mediator;
            _mapper = mapper;
            _storeContext = storeContext;
        }

        /// <summary>
        /// Creates statement without saving to database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Guid of the created statement</returns>
        public async Task<Guid> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(StatementCreating.Create(), cancellationToken);

            // Prepare statement for mapping
            if (request.Statement.Id.HasValue)
            {
                bool exist = await _dbContext.Statements.OfType<StatementModel>()
                    .AnyAsync(x =>
                        x.StoreId == _dbContext.StoreId
                        && x.Id == request.Statement.Id.Value,
                    cancellationToken);

                if (exist)
                {
                    return request.Statement.Id.Value;
                }
            }

            request.Statement.Stamp();

            // Ensure statement version and stored date
            request.Statement.Version = request.Statement.Version ?? ApiVersion.GetLatest().ToString();
            request.Statement.Stored = request.Statement.Stored ?? DateTimeOffset.UtcNow;

            if (request.Statement.Authority == null)
            {
                request.Statement.Authority = new Agent(_storeContext.GetClientAuthority());
            }
            else
            {
                // TODO: Validate authority
                // Find client in store, where authority is a match.
                // Check if client is enabled, and has rights to this store.
            }

            // Start mapping statement
            StatementModel newStatement = new StatementModel();
            newStatement.Id = request.Statement.Id.GetValueOrDefault();

            await HandleBase(request.Statement, newStatement, cancellationToken);

            newStatement.CreatedAt = request.Statement.Stored;
            newStatement.Client = _clientHttpContext.GetClient();
            //newStatement.Version = request.Statement.Version.ToString();

            _dbContext.Statements.Add(newStatement);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(StatementCreated.Create(newStatement), cancellationToken);

            return newStatement.Id;
        }

        private async Task HandleBase(StatementBase statement, Domain.Models.StatementBaseModel newStatement, CancellationToken cancellationToken)
        {
            newStatement.Verb = await _mediator.Send(UpsertVerbCommand.Create(statement.Verb), cancellationToken);

            newStatement.Persona = await HandleActor(statement.Actor, cancellationToken);

            await HandleContext(statement.Context, newStatement, cancellationToken);

            await HandleObject(statement.Object, newStatement, cancellationToken);

            if (statement.Result != null)
            {
                newStatement.Result = _mapper.Map<ResultModel>(statement.Result);
            }


            newStatement.Timestamp = statement.Timestamp;
        }

        private async Task HandleContext(Context context, StatementBaseModel stmt, CancellationToken cancellationToken)
        {
            if (context != null)
            {
                stmt.Context = _mapper.Map<StatementContext>(context);
                if (stmt.Context.Instructor != null)
                {
                    var instructor = await HandleActor(context.Instructor, cancellationToken);
                    stmt.Context.Instructor = instructor;
                    await CreateRelations(stmt.StatementId, instructor.ObjectType, instructor.PersonaId, cancellationToken);
                }
                if (stmt.Context.Team != null)
                {
                    var team = await HandleActor(context.Team, cancellationToken);
                    await CreateRelations(stmt.StatementId, team.ObjectType, team.PersonaId, cancellationToken);
                    stmt.Context.Team = team;
                }
            }
        }

        private async Task HandleObject(ExperienceApi.Data.IStatementObject @object, StatementBaseModel newStatement, CancellationToken cancellationToken)
        {
            // Serialize object as json
            //newStatement.Object = @object.ToJson();
            var objType = @object.ObjectType;
            if (@object is Activity activity)
            {
                var ac = await HandleActivity(activity, newStatement, cancellationToken);
                newStatement.ObjectType = Domain.Models.ObjectType.Activity;
                newStatement.ObjectId = ac.ActivityId;
            }
            else if (@object is SubStatement subStatement)
            {
                var model = await HandleSubStatement(subStatement, newStatement, cancellationToken);
                newStatement.ObjectType = model.ObjectType;
                newStatement.ObjectId = model.ObjectId;
                await CreateRelations(newStatement.StatementId, Domain.Models.ObjectType.SubStatement, model.StatementId, cancellationToken);
            }
            else if (@object is StatementRef statementRef)
            {
                //var saved = await HandleStatementRef(statementRef, newStatement, cancellationToken);
                newStatement.ObjectType = Domain.Models.ObjectType.StatementRef;
                newStatement.ObjectId = statementRef.Id;
                //await CreateRelations(newStatement.StatementId, Domain.Models.ObjectType.StatementRef, )
            }
            else
            {
                //Agen or group
                var persona = await HandleActor((Agent)@object, cancellationToken);
                newStatement.ObjectType = persona.ObjectType;
                newStatement.ObjectId = persona.PersonaId;
            }
        }

        private async Task<SubStatementEntity> HandleSubStatement(SubStatement subStatement, StatementBaseModel parent, CancellationToken cancellationToken)
        {
            var newSubStatement = new SubStatementEntity();
            await HandleBase(subStatement, newSubStatement, cancellationToken);
            _dbContext.Statements.Add(newSubStatement);
            return newSubStatement;
        }

        private async Task<Persona> HandleActor(Agent actor, CancellationToken cancellationToken)
        {
            return await _mediator.Send(UpsertActorCommand.Create(actor));
        }

        private async Task CreateRelations(Guid parentId, Domain.Models.ObjectType objectType, Guid childId, CancellationToken cancellationToken)
        {
            var relation = new StatementRelation()
            {
                ObjectType = objectType.ToString(),
                ParentId = parentId,
                ChildId = childId,
                StoreId = _dbContext.StoreId,
            };

            StatementRelation currentRelation = await _dbContext.Relations.SingleOrDefaultAsync(x =>
                 x.ObjectType == relation.ObjectType
                 && x.ParentId == relation.ParentId
                 && x.StoreId == relation.StoreId
            , cancellationToken);

            if (currentRelation != null)
            {
                return;
            }

            await _dbContext.Relations.AddAsync(relation, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<ActivityModel> HandleActivity(Activity requestActivity, StatementBaseModel newStatement, CancellationToken cancellationToken)
        {
            ActivityModel activity = (ActivityModel)await _mediator.Send(UpsertActivityCommand.Create(requestActivity), cancellationToken);

            await CreateRelations(newStatement.StatementId, Domain.Models.ObjectType.Activity, activity.ActivityId, cancellationToken);

            return activity;
        }
    }
}
