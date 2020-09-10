using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonQuery : IRequest<Person>
    {
        public Agent Agent { get; set; }

        public static GetPersonQuery Create(Agent agent)
        {
            return new GetPersonQuery()
            {
                Agent = agent
            };
        }
    }
}
