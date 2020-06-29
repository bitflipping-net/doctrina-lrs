using AutoMapper;
using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles
{
    public class DeleteAgentProfileHandler : IRequestHandler<DeleteAgentProfileCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public DeleteAgentProfileHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var agentEntity = await _mediator.Send(GetAgentQuery.Create(request.Agent));

            if(agentEntity == null)
            {
                return await Unit.Task;
            }

            AgentProfileEntity profile = await _context.AgentProfiles
                            .AsNoTracking()
                            .Where(x => x.AgentId == agentEntity.AgentId)
                            .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId, cancellationToken);

            if (profile != null)
            {
                _context.AgentProfiles.Remove(profile);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }
    }
}
