using AutoMapper;
using Doctrina.Domain.Entities.OwnedTypes;

namespace Doctrina.Application.Mappings.ValueResolvers
{
    using Doctrina.ExperienceApi.Data;

    public class ExtenstionsValueResolver :
        IMemberValueResolver<object, object, ExtensionsCollection, ExtensionsDictionary>,
        IMemberValueResolver<object, object, ExtensionsDictionary, ExtensionsCollection>
    {
        public ExtensionsCollection Resolve(object source, object destination, ExtensionsDictionary sourceMember, ExtensionsCollection destMember, ResolutionContext context)
        {
            if(sourceMember == null)
            {
                return null;
            }

            var collection = new ExtensionsCollection();

            foreach (var p in sourceMember)
            {
                collection.Add(p.Key, p.Value);
            }
            return collection;
        }

        public ExtensionsDictionary Resolve(object source, object destination, ExtensionsCollection sourceMember, ExtensionsDictionary destMember, ResolutionContext context)
        {
            var ext = new ExtensionsDictionary();
            foreach(var mem in sourceMember)
            {
                ext.Add(mem);
            }
            return ext;
        }
    }
}
