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

            var query = _context.Agents
                .Where(x => x.ObjectType == agent.ObjectType);

            return await query.SingleOrDefaultAsync(x =>
                x.ObjectType == agent.ObjectType &&
                x.IFI_Key == agent.IFI_Key &&
                x.IFI_Value == agent.IFI_Value
            , cancellationToken);
        }
    }
}