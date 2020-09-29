using AutoMapper;
using AutoMapper.Internal;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class ContextActivityConverter : ITypeConverter<ContextActivities, ICollection<ContextActivity>>,
        ITypeConverter<ICollection<ContextActivity>, ContextActivities>
    {
        public ICollection<ContextActivity> Convert(ContextActivities source, ICollection<ContextActivity> destination, ResolutionContext resolutionContext)
        {
            var result = new HashSet<ContextActivity>();
            foreach (var item in source.Category)
            {
                result.Add(new ContextActivity()
                {
                    ContextType = ContextType.Category,
                    Activity = resolutionContext.Mapper.Map<ActivityModel>(item)
                });
            }

            foreach (var item in source.Parent)
            {
                result.Add(new ContextActivity()
                {
                    ContextType = ContextType.Parent,
                    Activity = resolutionContext.Mapper.Map<ActivityModel>(item)
                });
            }

            foreach (var item in source.Other)
            {
                result.Add(new ContextActivity()
                {
                    ContextType = ContextType.Other,
                    Activity = resolutionContext.Mapper.Map<ActivityModel>(item)
                });
            }

            foreach (var item in source.Grouping)
            {
                result.Add(new ContextActivity()
                {
                    ContextType = ContextType.Grouping,
                    Activity = resolutionContext.Mapper.Map<ActivityModel>(item)
                });
            }

            return result;
        }

        public ContextActivities Convert(ICollection<ContextActivity> source, ContextActivities destination, ResolutionContext resolutionContext)
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
                    case ContextType.None:
                    default:
                        throw new NotImplementedException();
                }

                foreach (var item in group.AsEnumerable())
                    collection.Add(new Activity() { Id = new Iri(item.Activity.Iri) });
            }

            return context;
        }
    }
}
