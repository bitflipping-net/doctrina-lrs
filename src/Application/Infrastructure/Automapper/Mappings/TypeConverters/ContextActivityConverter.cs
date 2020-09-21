using AutoMapper;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class ContextActivityConverter : ITypeConverter<ContextActivities, ICollection<ContextActivity>>,
        ITypeConverter<ICollection<ContextActivity>, ContextActivities>
    {
        public ICollection<ContextActivity> Convert(ContextActivities source, ICollection<ContextActivity> destination, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }

        public ContextActivities Convert(ICollection<ContextActivity> source, ContextActivities destination, ResolutionContext context)
        {
            var grouping = source.GroupBy(x => x.ContextType);
            foreach(var group in grouping)
            {
                group.Key
            }
        }
    }
}