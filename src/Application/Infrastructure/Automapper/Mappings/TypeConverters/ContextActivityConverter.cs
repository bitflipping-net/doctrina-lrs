using AutoMapper;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class ContextActivityConverter : ITypeConverter<ContextActivities, ICollection<ContextActivityEntity>>,
        ITypeConverter<ICollection<ContextActivityEntity>, ContextActivities>
    {
        public ICollection<ContextActivityEntity> Convert(ContextActivities source, ICollection<ContextActivityEntity> destination, ResolutionContext resolutionContext)
        {
            var result = new HashSet<ContextActivityEntity>();

            if (source == null)
            {
                return null;
            }

            if (source.Category != null)
            {
                foreach (var item in source.Category)
                {
                    result.Add(new ContextActivityEntity()
                    {
                        ContextType = ContextType.Category,
                        Activity = resolutionContext.Mapper.Map<ActivityEntity>(item)
                    });
                }
            }

            if (source.Parent != null)
            {
                foreach (var item in source.Parent)
                {
                    result.Add(new ContextActivityEntity()
                    {
                        ContextType = ContextType.Parent,
                        Activity = resolutionContext.Mapper.Map<ActivityEntity>(item)
                    });
                }
            }

            if (source.Other != null)
            {
                foreach (var item in source.Other)
                {
                    result.Add(new ContextActivityEntity()
                    {
                        ContextType = ContextType.Other,
                        Activity = resolutionContext.Mapper.Map<ActivityEntity>(item)
                    });
                }
            }

            if (source.Grouping != null)
            {
                foreach (var item in source.Grouping)
                {
                    result.Add(new ContextActivityEntity()
                    {
                        ContextType = ContextType.Grouping,
                        Activity = resolutionContext.Mapper.Map<ActivityEntity>(item)
                    });
                }
            }

            return result;
        }

        public ContextActivities Convert(ICollection<ContextActivityEntity> source, ContextActivities destination, ResolutionContext resolutionContext)
        {
            var context = new ContextActivities();

            var grouping = source.GroupBy(x => x.ContextType);
            foreach (var group in grouping)
            {
                var collection = new ActivityCollection();
                switch (group.Key)
                {
                    case ContextType.Parent:
                        if (context.Parent == null)
                            context.Parent = new ActivityCollection();
                        collection = context.Parent;
                        break;
                    case ContextType.Grouping:
                        if (context.Grouping == null)
                            context.Grouping = new ActivityCollection();
                        collection = context.Grouping;
                        break;
                    case ContextType.Category:
                        if (context.Category == null)
                            context.Category = new ActivityCollection();
                        collection = context.Category;
                        break;
                    case ContextType.Other:
                        if (context.Other == null)
                            context.Other = new ActivityCollection();
                        collection = context.Other;
                        break;
                    case ContextType.Invalid:
                    default:
                        throw new NotImplementedException();
                }

                foreach (var item in group.AsEnumerable())
                    collection.Add(new Activity() { Id = new Iri(item.Activity.Id) });
            }

            return context;
        }
    }
}
