using Doctrina.Domain.Entities.OwnedTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class LanguageMapCollectionValueConverter : ValueConverter<LanguageMapCollection, string>
    {
        public LanguageMapCollectionValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<LanguageMapCollection, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, LanguageMapCollection>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(LanguageMapCollection extensions)
        {
            return JsonConvert.SerializeObject(extensions);
        }

        public static LanguageMapCollection FromDataStore(string strExtesions)
        {
            return JsonConvert.DeserializeObject<LanguageMapCollection>(strExtesions);
        }
    }
}
