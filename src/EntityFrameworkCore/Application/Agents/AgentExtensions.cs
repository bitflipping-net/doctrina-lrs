using Doctrina.Application.Infrastructure;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents
{
    public static class AgentExtensions
    {
        public static async Task<AgentEntity> SingleOrDefaultAsync(this IQueryable<AgentEntity> query, AgentEntity agent, CancellationToken cancellationToken)
        {
            query = query.Where(x => x.ObjectType == agent.ObjectType);

            if (agent.Mbox != null)
            {
                string mbox = agent.Mbox;
                return await query.SingleOrDefaultAsync(x => x.Mbox == mbox, cancellationToken);
            }
            else if (agent.Mbox_SHA1SUM != null)
            {
                string mbox_sha1sum = agent.Mbox_SHA1SUM;
                return await query.SingleOrDefaultAsync(x => x.Mbox_SHA1SUM == mbox_sha1sum, cancellationToken);
            }
            else if (agent.OpenId != null)
            {
                var openId = agent.OpenId;
                return await query.SingleOrDefaultAsync(x => x.OpenId == openId, cancellationToken);
            }
            else if (agent.Account != null)
            {
                var ac = agent.Account;
                string accountHomepage = ac.HomePage;
                string accountName = ac.Name;
                return await query.SingleOrDefaultAsync(x => x.Account.HomePage == accountHomepage && x.Account.Name == accountName, cancellationToken);
            }
            else if (agent.ObjectType == Domain.Entities.ObjectType.Agent)
            {
                throw new ArgumentException("Agent must have an identifier");
            }
            else
            {
                return null;
            }
        }

        public static string GetAgentCacheKeyIdentifier(this Agent agent)
        {
            Func<Agent, string> GetIFI = (Agent Agent) =>
            {
                if (Agent.Mbox != null)
                {
                    return Agent.Mbox.ToString();
                }
                else if (Agent.Mbox_SHA1SUM != null)
                {
                    return Agent.Mbox_SHA1SUM.ToString();
                }
                else if (Agent.OpenId != null)
                {
                    return Agent.OpenId.ToString().ComputeHash();
                }
                else if (Agent.Account != null)
                {
                    return $"{Agent.Account.HomePage}_{Agent.Account.Name}";
                }

                // Anonmouys group - return checksum
                return $"{Agent.ToJson().ComputeHash()}";
            };

            return $"{agent.ObjectType}_{GetIFI(agent)}";
        }
    }
}