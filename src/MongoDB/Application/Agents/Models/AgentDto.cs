using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using System;

namespace Doctrina.Application.Agents.Models
{
    public class AgentDto
    {
        public Guid AgentId { get; set; }

        public string Name { get; set; }

        public string Mbox { get; set; }

        public string Mbox_SHA1SUM { get; set; }

        public string OpenId { get; set; }

        public Account Account { get; set; }
    }
}
