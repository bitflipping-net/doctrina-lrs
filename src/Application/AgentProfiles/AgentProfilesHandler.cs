using AutoMapper;
using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Domain.Entities.Extensions;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles
{
    public class AgentProfilesHandler :
        IRequestHandler<GetAgentProfilesQuery, ICollection<AgentProfileDocument>>,
        IRequestHandler<GetAgentProfileQuery, AgentProfileDocument>,
        IRequestHandler<DeleteAgentProfileCommand>,
        IRequestHandler<MergeAgentProfileCommand, AgentProfileDocument>,
        IRequestHandler<CreateAgentProfileCommand, AgentProfileDocument>,
        IRequestHandler<UpdateAgentProfileCommand, AgentProfileDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AgentProfilesHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ICollection<AgentProfileDocument>> Handle(GetAgentProfilesQuery request, CancellationToken cancellationToken)
        {
            var agentEntity = _mapper.Map<AgentEntity>(request.Agent);
            var query = _context.AgentProfiles
                .WhereAgent(a=> a.Agent, agentEntity);

            if (request.Since.HasValue)
            {
                query = query.Where(x => x.Document.LastModified >= request.Since.Value);
            }

            query = query.OrderByDescending(x => x.Document.LastModified);

            return _mapper.Map<ICollection<AgentProfileDocument>>(await query.ToListAsync(cancellationToken));
        }

        public async Task<AgentProfileDocument> Handle(GetAgentProfileQuery request, CancellationToken cancellationToken)
        {
            var agentEntity = _mapper.Map<AgentEntity>(request.Agent);
            var profile = await GetAgentProfile(agentEntity, request.ProfileId, cancellationToken);
            return _mapper.Map<AgentProfileDocument>(profile);
        }

        public async Task<Unit> Handle(DeleteAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var agentEntity = _mapper.Map<AgentEntity>(request.Agent);
            AgentProfileEntity profile = await GetAgentProfile(agentEntity, request.ProfileId, cancellationToken);

            if (profile != null)
            {
                _context.AgentProfiles.Remove(profile);
            }

            return await Unit.Task;
        }

        public async Task<AgentProfileDocument> Handle(MergeAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await Handle(GetAgentProfileQuery.Create(request.Agent, request.ProfileId), cancellationToken);
            if (profile == null)
            {
                return await Handle(
                    CreateAgentProfileCommand.Create(request.Agent, request.ProfileId, request.Content, request.ContentType),
                    cancellationToken);
            }

            return await Handle(UpdateAgentProfileCommand.Create(request.Agent, request.ProfileId, request.Content, request.ContentType), cancellationToken);
        }

        public async Task<AgentProfileDocument> Handle(CreateAgentProfileCommand request, CancellationToken cancellationToken)
        {

            var agent = await _mediator.Send(MergeActorCommand.Create(_mapper, request.Agent), cancellationToken);

            var profile = new AgentProfileEntity(request.Content, request.ContentType)
            {
                ProfileId = request.ProfileId,
                Agent = agent
            };

            _context.AgentProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AgentProfileDocument>(profile);
        }

        public async Task<AgentProfileDocument> Handle(UpdateAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var agentEntity = _mapper.Map<AgentEntity>(request.Agent);

            var profile = await GetAgentProfile(agentEntity, request.ProfileId, cancellationToken);
            profile.Document.UpdateDocument(request.Content, request.ContentType);

            _context.AgentProfiles.Update(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AgentProfileDocument>(profile);
        }

        private async Task<AgentProfileEntity> GetAgentProfile(AgentEntity agentEntity, string profileId, CancellationToken cancellationToken)
        {
            return await _context.AgentProfiles
                            .WhereAgent(x=> x.Agent, agentEntity)
                            .SingleOrDefaultAsync(x => x.ProfileId == profileId, cancellationToken);
        }
    }
}
