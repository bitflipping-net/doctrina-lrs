using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                .Where(x=> x.ObjectType == agent.ObjectType);

            try
            {
                if(agent.Mbox != null)
                {
                    string mbox = agent.Mbox;
                    return await query.SingleOrDefaultAsync(x => x.Mbox == mbox, cancellationToken);
                }
                else if(agent.Mbox_SHA1SUM != null)
                {
                    string mbox_sha1sum = agent.Mbox_SHA1SUM;
                    return await query.SingleOrDefaultAsync(x=> x.Mbox_SHA1SUM == mbox_sha1sum, cancellationToken);
                }
                else if(agent.OpenId != null)
                {
                    var openId = agent.OpenId;
                    return await  query.SingleOrDefaultAsync(x => x.OpenId == openId, cancellationToken);
                }
                else if(agent.Account != null)
                {
                    var ac = agent.Account;
                    string accountHomepage = ac.HomePage;
                    string accountName = ac.Name;

                    return await query.SingleOrDefaultAsync(x=>
                        x.Account.HomePage == accountHomepage
                        && x.Account.Name == accountName,
                        cancellationToken);
                }
                else if(agent.ObjectType == EntityObjectType.Agent)
                {
                    throw new ArgumentException("Agent must have an identifier");
                }
                else
                {
                    return null;
                }
            }
            catch(InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}