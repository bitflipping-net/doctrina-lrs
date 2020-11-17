using Doctrina.Domain.Entities;
using MediatR;
using System;

namespace Doctrina.Application.Persons.Queries
{
    public class GetPersonQuery : IRequest<PersonEntity>
    {
        public Guid AgentId { get; set; }
        public EntityObjectType ObjectType { get; set; }

        public static GetPersonQuery Create(EntityObjectType objectType, Guid agentId)
        {
            return new GetPersonQuery()
            {
                ObjectType = objectType,
                AgentId = agentId
            };
        }
    }
}
