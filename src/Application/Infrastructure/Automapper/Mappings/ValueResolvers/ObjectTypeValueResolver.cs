using AutoMapper;
using Doctrina.Domain.Entities;
using System;

namespace Doctrina.Application.Mappings.ValueResolvers
{
    using Doctrina.ExperienceApi.Data;

    public class ObjectTypeValueResolver :
         IMemberValueResolver<object, object, Domain.Entities.ObjectType, ObjectType>,
         IMemberValueResolver<object, object, ObjectType, Domain.Entities.ObjectType>
    {
        public Domain.Entities.ObjectType Resolve(object source, object destination, ObjectType sourceMember, Domain.Entities.ObjectType destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }

        public ObjectType Resolve(object source, object destination, Domain.Entities.ObjectType sourceMember, ObjectType destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
