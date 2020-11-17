using Doctrina.Domain.Entities;

namespace Doctrina.Application.Tests.Common
{
    public static class AgentsTestFixture
    {
        public static AgentEntity JamesCampbell()
        {
            var agent = new AgentEntity()
            {
                IFI_Key = Ifi.Account,
                IFI_Value = "{ \"homePage\":\"http://www.bitflipping.net/agentId/1\", \"name\":\"Josephina\" }"
            };
            return agent;
        }

        public static AgentEntity JosephinaCampbell() => new AgentEntity()
        {
            IFI_Key = Ifi.Mbox,
            IFI_Value = "mailto:james@bitflipping.net"
        };

    }
}