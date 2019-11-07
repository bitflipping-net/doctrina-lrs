using AutoMapper;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementCommand : IRequest<Guid>
    {
        public Statement Statement { get; set; }

        public static CreateStatementCommand Create(Statement statement)
        {
            return new CreateStatementCommand()
            {
                Statement = statement
            };
        }

        public class Handler : IRequestHandler<CreateStatementCommand, Guid>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public Handler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
            {
                _context = context;
                _mediator = mediator;
                _mapper = mapper;
            }

            /// <summary>
            /// Creates statement without saving to database
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns>Guid of the created statement</returns>
            public async Task<Guid> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
            {
                if (!request.Statement.Id.HasValue)
                {
                    request.Statement.Stamp();
                }
                else
                {
                    var current = await _context.Statements.FirstOrDefaultAsync(x => x.StatementId == request.Statement.Id, cancellationToken);
                    if(current != null)
                    {
                        return current.StatementId.Value;
                    }
                }

                // Ensure statement version and stored date
                request.Statement.Version = request.Statement.Version ?? ApiVersion.GetLatest().ToString();
                request.Statement.Stored = request.Statement.Stored ?? DateTimeOffset.UtcNow;

                // TODO: Move this logic elsewhere
                //var httpRequest = _httpContextAccessor.HttpContext.Request;
                //var url = new Uri($"{httpRequest.Scheme}://{httpRequest.Host.Value}");
                //if (request.Statement.Authority == null)
                //{
                //    request.Statement.Authority = new Agent()
                //    {
                //        Account = new xAPI.Account()
                //        {
                //            HomePage = url,
                //            Name = "REPLACE ME"
                //        }
                //    };
                //}

                StatementEntity statement = _mapper.Map<StatementEntity>(request.Statement);
                statement.Verb = await _mediator.Send(MergeVerbCommand.Create(statement.Verb), cancellationToken);
                statement.Actor = await _mediator.Send(MergeActorCommand.Create(statement.Actor), cancellationToken);
                statement.FullStatement = request.Statement.ToJson();

                _context.Statements.Add(statement);

                return statement.StatementId.Value;
            }
        }
    }
}
