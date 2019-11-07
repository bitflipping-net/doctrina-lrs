using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfilesQuery : IRequest<ICollection<AgentProfileDocument>>
    {
        public Agent Agent { get; set; }
        public DateTimeOffset? Since { get; set; }

        public GetAgentProfilesQuery(Agent agent, DateTimeOffset? since)
        {
            this.Agent = agent;
            this.Since = since;
        }
    }
}
