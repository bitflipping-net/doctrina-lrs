using AutoMapper;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Persons.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Resources
{
    public class AgentResource : IAgentResource
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AgentResource(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Person> GetPerson(Agent agent, CancellationToken cancellationToken = default)
        {
            var fallback = new Person();
            fallback.Add(agent);

            var storedAgent = await mediator.Send(GetAgentQuery.Create(agent));

            if (storedAgent == null)
                return fallback;

            var person = await mediator.Send(GetPersonQuery.Create(storedAgent.ObjectType, storedAgent.AgentId), cancellationToken);
            if (person == null)
                return fallback;

            return mapper.Map<Person>(person);
        }
    }
}
