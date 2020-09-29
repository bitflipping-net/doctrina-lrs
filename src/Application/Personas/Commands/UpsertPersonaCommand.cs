using Doctrina.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.Personas.Commands
{
    public class UpsertPersonaCommand : IRequest<Persona>
    {
        public Guid StoreId { get; private set; }
        public ObjectType Type { get; private set; }
        public string Name { get; private set; }
        public InverseFunctionalIdentifier Key { get; private set; }
        public string Value { get; private set; }
        public IEnumerable<Persona> Personas { get; private set; }

        /// <summary>
        /// Create a group without IFI
        /// </summary>
        /// <returns></returns>
        public static UpsertPersonaCommand CreateAnonymousGroup(Guid storeId, IEnumerable<Persona> members, string name = null)
        {
            return new UpsertPersonaCommand()
            {
                StoreId = storeId,
                Type = ObjectType.Group,
                Personas = members,
                Name = name,
            };
        }

        /// <summary>
        /// Create an Identified Group
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="members"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static UpsertPersonaCommand CreateIdentifiedGroup(Guid storeId, InverseFunctionalIdentifier key, string value, IEnumerable<Persona> members, string name = null)
        {
            return new UpsertPersonaCommand()
            {
                StoreId = storeId,
                Type = ObjectType.Group,
                Key = key,
                Value = value,
                Personas = members,
                Name = name,
            };
        }

        /// <summary>
        /// Create agent
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static UpsertPersonaCommand CreateAgent(Guid storeId, InverseFunctionalIdentifier key, string value, string name = null)
        {
            return new UpsertPersonaCommand()
            {
                StoreId = storeId,
                Type = ObjectType.Agent,
                Key = key,
                Value = value,
                Name = name
            };
        }
    }
}
