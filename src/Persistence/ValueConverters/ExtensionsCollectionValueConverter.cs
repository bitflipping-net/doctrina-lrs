using Doctrina.Domain.Entities.OwnedTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class ExtensionsCollectionValueConverter : ValueConverter<ExtensionsCollection, string>
    {
        public ExtensionsCollectionValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<ExtensionsCollection, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, ExtensionsCollection>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(ExtensionsCollection extensions)
        {
            return JsonConvert.SerializeObject(extensions);
        }

        public static ExtensionsCollection FromDataStore(string strExtesions)
        {
            return JsonConvert.DeserializeObject<ExtensionsCollection>(strExtesions);
        }
    }
}
