using AutoMapper;
using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentQueryHandler : IRequestHandler<GetAgentQuery, AgentEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private IMapper _mapper;

        public GetAgentQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgentEntity> Handle(GetAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = _mapper.Map<AgentEntity>(request.Agent);

            if (string.IsNullOrEmpty(agent.IFI_Key) || string.IsNullOrEmpty(agent.IFI_Value))
                return null;

            if (agent.ObjectType == EntityObjectType.Group)
            {
                var query = _context.Agents.OfType<GroupEntity>()
                .AsNoTracking()
                .Include(x => x.Members)
                .Where(x =>
                    x.ObjectType == agent.ObjectType &&
                    x.IFI_Key == agent.IFI_Key &&
                    x.IFI_Value == agent.IFI_Value);

                return await query.SingleOrDefaultAsync(cancellationToken);
            }
            else
            {
                var query = _context.Agents
                .AsNoTracking()
                .Where(x =>
                    x.ObjectType == agent.ObjectType &&
                    x.IFI_Key == agent.IFI_Key &&
                    x.IFI_Value == agent.IFI_Value);

                return await query.SingleOrDefaultAsync(cancellationToken);
            }

        }
    }
}