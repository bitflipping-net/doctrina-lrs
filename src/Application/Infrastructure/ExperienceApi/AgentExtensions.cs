using System;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.ExperienceApi
{
    public static class AgentExtensions
    {
        public static bool IsAnonymous(this Agent agent)
        {
            return agent.IsAnonymous();
        }

        /// <summary>
        /// Gets the <see cref="InverseFunctionalIdentifier"/> for the agent
        /// </summary>
        public static InverseFunctionalIdentifier GetIdentifierKey(this Agent agent)
        {
            if (agent.Mbox != null)
            {
                return InverseFunctionalIdentifier.Mbox;
            }

            if (agent.Mbox_SHA1SUM != null)
            {
                return InverseFunctionalIdentifier.Mbox_SHA1SUM;
            }

            if (agent.OpenId != null)
            {
                return InverseFunctionalIdentifier.OpenId;
            }

            if (agent.Account != null)
            {
                return InverseFunctionalIdentifier.Account;
            }

            return null;
        }

        /// <summary>
        /// Gets the value of the <see cref="InverseFunctionalIdentifier"/>
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
