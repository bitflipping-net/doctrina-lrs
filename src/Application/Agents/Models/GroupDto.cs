using System.Collections.Generic;

namespace Doctrina.Application.Agents.Models
{
    public class GroupDto : AgentDto
    {
        public GroupDto()
        {
            Members = new HashSet<AgentDto>();
        }

        public ICollection<AgentDto> Members { get; set; }
    }
}
