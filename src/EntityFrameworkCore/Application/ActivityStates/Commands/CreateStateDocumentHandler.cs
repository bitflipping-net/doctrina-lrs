using AutoMapper;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class CreateStateDocumentHandler : IRequestHandler<CreateStateDocumentCommand, ActivityStateEntity>
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

        public async Task<ActivityStateEntity> Handle(CreateStateDocumentCommand request, CancellationToken cancellationToken)
        {
            var state = new ActivityStateEntity(request.Content, request.ContentType)
            {
                Key = request.StateId,
                ActivityId = request.ActivityId,
                AgentId = request.AgentId,
                RegistrationId = request.Registration
            };

            _context.Documents.Add(state);
            await _context.SaveChangesAsync(cancellationToken);

            return state;
        }
    }


}
