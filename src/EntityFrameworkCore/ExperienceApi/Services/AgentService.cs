using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi
{
    public class AgentService : IAgentService
    {
        public Task<Person> GetPerson(Agent agent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
