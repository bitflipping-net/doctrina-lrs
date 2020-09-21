using AutoMapper;
using Doctrina.Domain.Entities.ValueObjects;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class LanuageMapTypeConverter : ITypeConverter<LanguageMap, LanguageMapCollection>,
        ITypeConverter<LanguageMapCollection, LanguageMap>,
        ITypeConverter<LanguageMapCollection, LanguageMapCollection>
    {
        public LanguageMapCollection Convert(LanguageMap source, LanguageMapCollection destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }
            if (destination == null)
            {
                destination = new LanguageMapCollection();
            }
            foreach (var sourceItem in source)
            {
                destination[sourceItem.Key] = sourceItem.Value;
            }
            return destination;
        }

        public LanguageMap Convert(LanguageMapCollection source, LanguageMap destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }
            if (destination == null)
            {
                destination = new LanguageMap();
            }
            foreach (var sourceItem in source)
            {
                destination[sourceItem.Key] = sourceItem.Value;
            }
            return destination;
        }

        public LanguageMapCollection Convert(LanguageMapCollection source, LanguageMapCollection destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }
            if (destination == null)
            {
                destination = new LanguageMapCollection();
            }
            foreach (var sourceItem in source)
            {
                destination[sourceItem.Key] = sourceItem.Value;
            }
            return destination;
        }
    }
}