using System;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.ExperienceApi{
    public static class AgentExtensions
    {
        public static bool IsAnonymous(this Agent agent)
        {
            return agent.IsAnonymous();
        }

        public static Domain.Entities.EntityObjectType GetObjectType(this Agent agent)
        {
            Domain.Entities.EntityObjectType parsed = (Domain.Entities.EntityObjectType)Enum.Parse(
                typeof(Domain.Entities.EntityObjectType),
                agent.ObjectType.ToString()
            );
            return parsed;
        }

        /// <summary>
        /// Gets the <see cref="Ifi"/> for the agent
        /// </summary>
        public static Ifi GetIdentifierKey(this Agent agent)
        {
            if (agent.Mbox != null)
            {
                return Ifi.Mbox;
            }

            if (agent.Mbox_SHA1SUM != null)
            {
                return Ifi.Mbox_SHA1SUM;
            }

            if (agent.OpenId != null)
            {
                return Ifi.OpenId;
            }

            if (agent.Account != null)
            {
                return Ifi.Account;
            }

            return null;
        }

        /// <summary>
        /// Gets the value of the <see cref="Ifi"/>
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public static string GetIdentifierValue(this Agent agent)
        {
            if (agent.Mbox != null)
            {
                return agent.Mbox.ToString();
            }

            if (agent.Mbox_SHA1SUM != null)
            {
                return agent.Mbox_SHA1SUM;
            }

            if (agent.OpenId != null)
            {
                return agent.OpenId.ToString();
            }

            if (agent.Account != null)
            {
                var uriBuilder = new UriBuilder(agent.Account.HomePage)
                {
                    UserName = agent.Account.Name
                };
                return uriBuilder.ToString();
            }

            return null;
        }
    }
}