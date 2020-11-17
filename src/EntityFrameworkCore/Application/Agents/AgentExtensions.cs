using Doctrina.Application.Infrastructure;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.Application.Infrastructure.ExperienceApi;

namespace Doctrina.Application.Agents
{
    public static class AgentExtensions
    {
        public static Task<AgentEntity> SingleOrDefaultAsync(this IQueryable<AgentEntity> query, AgentEntity agent, CancellationToken cancellationToken)
        {
            return query.SingleOrDefaultAsync(x =>
                x.ObjectType == agent.ObjectType &&
                x.IFI_Key == agent.IFI_Key &&
                x.IFI_Value == agent.IFI_Value
            , cancellationToken);
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