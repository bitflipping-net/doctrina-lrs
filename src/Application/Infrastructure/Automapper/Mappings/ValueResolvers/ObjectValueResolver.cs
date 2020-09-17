﻿using AutoMapper;
using Doctrina.Domain.Entities;
using System;

namespace Doctrina.Application.Mappings.ValueResolvers
{
    using Doctrina.ExperienceApi.Data;

    public class ObjectValueResolver :
         IMemberValueResolver<object, object, StatementObjectEntity, IStatementObject>,
         IMemberValueResolver<object, object, IStatementObject, StatementObjectEntity>
    {
        public IStatementObject Resolve(object source, object destination, Domain.Entities.StatementObjectEntity sourceMember, IStatementObject destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return null;
            }

            if (sourceMember.ObjectType == Domain.Entities.ObjectType.Agent)
            {
                return context.Mapper.Map<Agent>(sourceMember.Agent);
            }
            else if (sourceMember.ObjectType == Domain.Entities.ObjectType.Group)
            {
                return context.Mapper.Map<Group>(sourceMember.Agent);
            }
            else if (sourceMember.ObjectType == Domain.Entities.ObjectType.Activity)
            {
                return context.Mapper.Map<Activity>(sourceMember.Activity);
            }
            else if (sourceMember.ObjectType == Domain.Entities.ObjectType.SubStatement)
            {
                return context.Mapper.Map<SubStatement>(sourceMember.SubStatement);
            }
            else if (sourceMember.ObjectType == Domain.Entities.ObjectType.StatementRef)
            {
                return context.Mapper.Map<StatementRef>(sourceMember.StatementRef);
            }

            throw new NotImplementedException();
        }

        public Domain.Entities.StatementObjectEntity Resolve(object source, object destination, IStatementObject sourceMember, Domain.Entities.StatementObjectEntity destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return null;
            }

            try
            {
                var obj = new StatementObjectEntity();

                if (sourceMember.ObjectType == ObjectType.Agent)
                {
                    obj.ObjectType = Domain.Entities.ObjectType.Agent;
                    obj.Agent = context.Mapper.Map<AgentEntity>((Agent)sourceMember);
                    return obj;
                }
                else if (sourceMember.ObjectType == ObjectType.Group)
                {
                    obj.ObjectType = Domain.Entities.ObjectType.Group;
                    obj.Agent = context.Mapper.Map<GroupEntity>((Group)sourceMember);
                    return obj;
                }
                else if (sourceMember.ObjectType == ObjectType.Activity)
                {
                    obj.ObjectType = Domain.Entities.ObjectType.Activity;
                    obj.Activity = context.Mapper.Map<ActivityEntity>((Activity)sourceMember);
                    return obj;
                }
                else if (sourceMember.ObjectType == ObjectType.SubStatement)
                {
                    obj.ObjectType = Domain.Entities.ObjectType.SubStatement;
                    obj.SubStatement = context.Mapper.Map<SubStatementEntity>((SubStatement)sourceMember);
                    return obj;
                }
                else if (sourceMember.ObjectType == ObjectType.StatementRef)
                {
                    obj.ObjectType = Domain.Entities.ObjectType.StatementRef;
                    obj.StatementRef = context.Mapper.Map<StatementRefEntity>((StatementRef)sourceMember);
                    return obj;
                }
            }
            catch (Exception)
            {
                throw;
            }

            throw new NotImplementedException();
        }
    }
}
