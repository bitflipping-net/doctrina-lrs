using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.ActivityStates.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Domain.Entities.Extensions;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates
{
    public class ActivityStatesHandler :
        IRequestHandler<CreateStateDocumentCommand, ActivityStateDocument>,
        IRequestHandler<GetActivityStateQuery, ActivityStateDocument>,
        IRequestHandler<MergeStateDocumentCommand, ActivityStateDocument>,
        IRequestHandler<DeleteActivityStateCommand>,
        IRequestHandler<DeleteActivityStatesCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ActivityStatesHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<ActivityStateDocument> Handle(CreateStateDocumentCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(MergeActivityIriCommand.Create(request.ActivityId), cancellationToken);
            var agent = await _mediator.Send(MergeActorCommand.Create(_mapper, request.Agent), cancellationToken);

            var state = new ActivityStateEntity(request.Content, request.ContentType)
            {
                StateId = request.StateId,
                Activity = activity,
                Agent = agent,
                Registration = request.Registration
            };

            _context.ActivityStates.Add(state);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityStateDocument>(state);
        }

        public async Task<ActivityStateDocument> Handle(GetActivityStateQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();

            AgentEntity agent = _mapper.Map<AgentEntity>(request.Agent);

            var query = _context.ActivityStates
                .Where(x => x.Activity.Hash == activityHash)
                .WhereAgent(x => x.Agent, agent);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.Registration == request.Registration);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync(cancellationToken);

            return _mapper.Map<ActivityStateDocument>(state);
        }

        public async Task<ActivityStateDocument> Handle(MergeStateDocumentCommand request, CancellationToken cancellationToken)
        {
            AgentEntity agent = _mapper.Map<AgentEntity>(request.Agent);
            string activityHash = request.ActivityId.ComputeHash();
            var query = _context.ActivityStates
                .Where(x => x.Activity.Hash == activityHash)
                .WhereAgent(x => x.Agent, agent);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.Registration == request.Registration);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync();

            if (state != null)
            {
                // Update
                state.Document.UpdateDocument(request.Content, request.ContentType);
                _context.ActivityStates.Update(state);
                await _context.SaveChangesAsync(cancellationToken);
                return _mapper.Map<ActivityStateDocument>(state);
            }
            else
            {
                // Create
                return await Handle(new CreateStateDocumentCommand()
                {
                    ActivityId = request.ActivityId,
                    Agent = request.Agent,
                    Content = request.Content,
                    ContentType = request.ContentType,
                    Registration = request.Registration,
                    StateId = request.StateId
                }, cancellationToken);
            }
        }

        public async Task<Unit> Handle(DeleteActivityStateCommand request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            var agent = _mapper.Map<AgentEntity>(request.Agent);
            var activity = await _context.ActivityStates
                .Where(x => x.StateId == request.StateId && x.Activity.Hash == activityHash &&
                (!request.Registration.HasValue || x.Registration == request.Registration))
                .WhereAgent(x => x.Agent, agent)
                .FirstOrDefaultAsync(cancellationToken);

            if (activity != null)
            {
                _context.ActivityStates.Remove(activity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }

        public async Task<Unit> Handle(DeleteActivityStatesCommand request, CancellationToken cancellationToken)
        {
            var agent = _mapper.Map<AgentEntity>(request.Agent);
            string activityHash = request.ActivityId.ComputeHash();
            var activities = _context.ActivityStates.Where(x => x.Activity.Hash == activityHash)
                .WhereAgent(x => x.Agent, agent);

            _context.ActivityStates.RemoveRange(activities);

            return await Unit.Task;
        }
    }
}
