using AutoMapper;
using Doctrina.Domain.Entities;
using System;

namespace Doctrina.Application.Mappings.ValueResolvers
{
    using Doctrina.ExperienceApi.Data;

    public class ObjectTypeValueResolver :
         IMemberValueResolver<object, object, EntityObjectType, ObjectType>,
         IMemberValueResolver<object, object, ObjectType, EntityObjectType>
    {
        public EntityObjectType Resolve(object source, object destination, ObjectType sourceMember, EntityObjectType destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }

        public ObjectType Resolve(object source, object destination, EntityObjectType sourceMember, ObjectType destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
