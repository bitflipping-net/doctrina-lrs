using AutoMapper;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class ExtensionsTypeConverter : ITypeConverter<ExtensionsDictionary, ExtensionsCollection>,
        ITypeConverter<ExtensionsCollection, ExtensionsDictionary>
    {
        public ExtensionsCollection Convert(ExtensionsDictionary source, ExtensionsCollection destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }

            if (destination == null)
            {
                destination = new ExtensionsCollection();
            }

            foreach (var sourceItem in source)
            {
                destination[sourceItem.Key] = sourceItem.Value;
            }

            return destination;
        }

        public ExtensionsDictionary Convert(ExtensionsCollection source, ExtensionsDictionary destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }

            if (destination == null)
            {
                destination = new ExtensionsDictionary();
            }

            foreach (var sourceItem in source)
            {
                destination[sourceItem.Key] = sourceItem.Value;
            }

            return destination;
        }
    }
}