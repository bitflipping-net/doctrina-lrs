using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfilesQuery : IRequest<ICollection<AgentProfileEntity>>
    {
        public Agent Agent { get; set; }
        public DateTimeOffset? Since { get; set; }

        public static GetAgentProfilesQuery Create(Agent agent, DateTimeOffset? since)
        {
            return new GetAgentProfilesQuery()
            {
                Agent = agent,
                Since = since
            };
        }
    }
}
