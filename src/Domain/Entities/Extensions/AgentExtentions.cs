using System;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Domain.Entities.Extensions
{
    public static class AgentExtentions
    {
        public static IQueryable<T> WhereAgent<T>(this IQueryable<T> profiles, Func<T, AgentEntity> agentSelector, AgentEntity agent)
        {
            profiles = profiles.Where(p => agentSelector(p).ObjectType == agent.ObjectType);

            if (string.IsNullOrEmpty(agent.Mbox))
            {
                return profiles.Where(p => agentSelector(p).Mbox == agent.Mbox);
            }

            if (string.IsNullOrEmpty(agent.Mbox_SHA1SUM))
            {
                return profiles.Where(p => agentSelector(p).Mbox_SHA1SUM == agent.Mbox_SHA1SUM);
            }

            if (string.IsNullOrEmpty(agent.OpenId))
            {
                return profiles.Where(p => agentSelector(p).OpenId == agent.OpenId);
            }

            if (string.IsNullOrEmpty(agent.Account.HomePage))
            {
                return profiles.Where(p => agentSelector(p).Account.HomePage == agent.Account.HomePage && agentSelector(p).Account.Name == agent.Account.Name);
            }

            return profiles;
        }

        public static IEnumerable<T> WhereAgent<T>(this IEnumerable<T> profiles, Func<T, AgentEntity> agentSelector, AgentEntity agent)
        {
            profiles = profiles.Where(p => agentSelector(p).ObjectType == agent.ObjectType);

            if (string.IsNullOrEmpty(agent.Mbox))
            {
                return profiles.Where(p => agentSelector(p).Mbox == agent.Mbox);
            }

            if (string.IsNullOrEmpty(agent.Mbox_SHA1SUM))
            {
                return profiles.Where(p => agentSelector(p).Mbox_SHA1SUM == agent.Mbox_SHA1SUM);
            }

            if (string.IsNullOrEmpty(agent.OpenId))
            {
                return profiles.Where(p => agentSelector(p).OpenId == agent.OpenId);
            }

            if (string.IsNullOrEmpty(agent.Account.HomePage))
            {
                return profiles.Where(p => agentSelector(p).Account.HomePage == agent.Account.HomePage && agentSelector(p).Account.Name == agent.Account.Name);
            }

            return profiles;
        }
    }
}
