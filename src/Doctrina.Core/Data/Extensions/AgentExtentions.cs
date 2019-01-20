﻿using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Core.Data.Extensions
{
    public static class AgentExtentions
    {
        public static IEnumerable<T> WhereAgent<T>(this IQueryable<T> profiles, AgentEntity agent)
            where T : IQueryableAgent
        {
            if (string.IsNullOrEmpty(agent.Mbox))
            {
                return profiles.Where(x => x.Agent.Mbox == agent.Mbox);
            }

            if (string.IsNullOrEmpty(agent.Mbox_SHA1SUM))
            {
                return profiles.Where(x => x.Agent.Mbox_SHA1SUM == agent.Mbox_SHA1SUM);
            }

            if (string.IsNullOrEmpty(agent.OpenId))
            {
                return profiles.Where(x => x.Agent.OpenId == agent.OpenId);
            }

            if (string.IsNullOrEmpty(agent.Account_HomePage ))
            {
                return profiles.Where(x => x.Agent.Account_HomePage == agent.Account_HomePage && x.Agent.Account_Name == agent.Account_Name);
            }

            return null;
        }
    }
}
