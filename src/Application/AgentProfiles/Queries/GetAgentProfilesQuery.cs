using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfilesQuery : IRequest<ICollection<AgentProfileEntity>>
    {
        public Persona Persona { get; set; }
        public DateTimeOffset? Since { get; set; }

        public static GetAgentProfilesQuery Create(Persona persona, DateTimeOffset? since)
        {
            return new GetAgentProfilesQuery()
            {
                Persona = persona,
                Since = since
            };
        }
    }
}
