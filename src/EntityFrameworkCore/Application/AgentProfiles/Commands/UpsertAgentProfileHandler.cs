using AutoMapper;
using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles
{
    public class UpsertAgentProfileHandler : IRequestHandler<UpsertAgentProfileCommand, AgentProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpsertAgentProfileHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AgentProfileEntity> Handle(UpsertAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _mediator.Send(GetAgentProfileQuery.Create(request.AgentId, request.ProfileId), cancellationToken);
            if (profile == null)
            {
                return await _mediator.Send(
                    CreateAgentProfileCommand.Create(request.AgentId, request.ProfileId, request.Content, request.ContentType),
                cancellationToken);
            }

            return await _mediator.Send(UpdateAgentProfileCommand.Create(request.AgentId, request.ProfileId, request.Content, request.ContentType), cancellationToken);
        }
    }
}
