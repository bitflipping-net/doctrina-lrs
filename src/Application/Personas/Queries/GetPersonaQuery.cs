using System;
using Doctrina.Application.Infrastructure.ExperienceApi;
using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Personas.Queries
{
    public class GetPersonaQuery : IRequest<PersonaModel>
    {
        public ObjectType ObjectType { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        /// <summary>
        /// Create persona query
        /// </summary>
        /// <param name="objectType">Agent or Group</param>
        /// <param name="key">IFI Key (mbox, mbox_sha1sum, openid, account)</param>
        /// <param name="value">IFI value matching the key</param>
        public static GetPersonaQuery Create(ObjectType objectType, string key, string value)
        {
            return new GetPersonaQuery()
            {
                ObjectType = objectType,
                Key = key,
                Value = value
            };
        }

        /// <summary>
        /// Create persona query from xAPI Agent Object
        /// </summary>
        public static IRequest<PersonaModel> Create(ExperienceApi.Data.Agent agent)
        {
            return new GetPersonaQuery()
            {
                ObjectType = agent.GetObjectType(),
                Key = agent.GetIdentifierKey(),
                Value = agent.GetIdentifierValue()
            };
        }
    }
}
