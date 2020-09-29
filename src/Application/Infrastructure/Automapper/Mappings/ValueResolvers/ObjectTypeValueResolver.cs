using AutoMapper;
using Doctrina.Domain.Models;
using System;

namespace Doctrina.Application.Mappings.ValueResolvers
{
    using Doctrina.ExperienceApi.Data;

    public class ObjectTypeValueResolver :
         IMemberValueResolver<object, object, Domain.Models.ObjectType, ObjectType>,
         IMemberValueResolver<object, object, ObjectType, Domain.Models.ObjectType>
    {
        public Domain.Models.ObjectType Resolve(object source, object destination, ObjectType sourceMember, Domain.Models.ObjectType destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }

        public ObjectType Resolve(object source, object destination, Domain.Models.ObjectType sourceMember, ObjectType destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
