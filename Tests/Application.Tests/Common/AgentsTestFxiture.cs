using Doctrina.Domain.Entities;

namespace Doctrina.Application.Tests.Common
{
    public static class AgentsTestFixture
    {
        public static AgentEntity JamesCampbell()
        {
            var agent = new AgentEntity()
            {
                Account = new Domain.Entities.Account
                {
                    HomePage = "http://www.example.com/agentId/1",
                    Name = "James Campbell"
                }
            };
            return agent;
        }

        public static AgentEntity JosephinaCampbell() => new AgentEntity()
        {
            Account = new Domain.Entities.Account
            {
                HomePage = "http://www.example.com/agentId/2",
                Name = "Josephina Campbell"
            }
        };

    }
}