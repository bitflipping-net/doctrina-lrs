using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class MergeStateDocumentHandler : IRequestHandler<MergeStateDocumentCommand, ActivityStateDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MergeStateDocumentHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActivityStateDocument> Handle(MergeStateDocumentCommand request, CancellationToken cancellationToken)
        {
            AgentEntity agent = _mapper.Map<AgentEntity>(request.Agent);
            string activityHash = request.ActivityId.ComputeHash();
            var query = _context.ActivityStates
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.Hash == agent.Hash);

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
                return await _mediator.Send(new CreateStateDocumentCommand()
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
    }
}
