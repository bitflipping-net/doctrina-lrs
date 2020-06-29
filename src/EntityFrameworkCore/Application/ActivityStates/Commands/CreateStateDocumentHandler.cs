using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class CreateStateDocumentHandler : IRequestHandler<CreateStateDocumentCommand, ActivityStateDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateStateDocumentHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActivityStateDocument> Handle(CreateStateDocumentCommand request, CancellationToken cancellationToken)
        {

            var state = new ActivityStateEntity(request.Content, request.ContentType)
            {
                StateId = request.StateId,
                Activity = request.Activity,
                Agent = request.Agent,
                Registration = request.Registration
            };

            _context.ActivityStates.Add(state);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityStateDocument>(state);
        }
    }


}
