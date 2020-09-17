using AutoMapper;
using Doctrina.Domain.Entities;
using System;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    using Doctrina.ExperienceApi.Data;

    public class StatementObjectConverter :
    ITypeConverter<IStatementObject, StatementObjectEntity>,
    ITypeConverter<StatementObjectEntity, IStatementObject>
    {
        public StatementObjectEntity Convert(IStatementObject source, StatementObjectEntity destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new StatementObjectEntity();
            }

            if (source.ObjectType == ObjectType.Agent)
            {
                destination.ObjectType = Domain.Entities.ObjectType.Agent;
                destination.Agent = context.Mapper.Map<AgentEntity>((Agent)source);
            }
            else if (source.ObjectType == ObjectType.Group)
            {
                destination.ObjectType = Domain.Entities.ObjectType.Group;
                destination.Agent = context.Mapper.Map<GroupEntity>((Group)source);
            }
            else if (source.ObjectType == ObjectType.Activity)
            {
                destination.ObjectType = Domain.Entities.ObjectType.Activity;
                destination.Activity = context.Mapper.Map<ActivityEntity>((Activity)source);
            }
            else if (source.ObjectType == ObjectType.SubStatement)
            {
                destination.ObjectType = Domain.Entities.ObjectType.SubStatement;
                destination.SubStatement = context.Mapper.Map<SubStatementEntity>((SubStatement)source);
            }
            else if (source.ObjectType == ObjectType.StatementRef)
            {
                destination.ObjectType = Domain.Entities.ObjectType.StatementRef;
                destination.StatementRef = context.Mapper.Map<StatementRefEntity>((StatementRef)source);
            }

            return destination;
        }

        public IStatementObject Convert(StatementObjectEntity source, IStatementObject destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            if (source.ObjectType == Domain.Entities.ObjectType.Agent)
            {
                return context.Mapper.Map<Agent>(source.Agent);
            }
            else if (source.ObjectType == Domain.Entities.ObjectType.Group)
            {
                return context.Mapper.Map<Group>(source.Agent);
            }
            else if (source.ObjectType == Domain.Entities.ObjectType.Activity)
            {
                return context.Mapper.Map<Activity>(source.Activity);
            }
            else if (source.ObjectType == Domain.Entities.ObjectType.SubStatement)
            {
                return context.Mapper.Map<SubStatement>(source.SubStatement);
            }
            else if (source.ObjectType == Domain.Entities.ObjectType.StatementRef)
            {
                return context.Mapper.Map<StatementRef>(source.StatementRef);
            }

            throw new NotImplementedException();
        }
    }
}
