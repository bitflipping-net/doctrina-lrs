using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class StringArrayValueConverter : ValueConverter<string[], string>
    {
        public StringArrayValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<string[], string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, string[]>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(string[] extensions)
        {
            return JsonConvert.SerializeObject(extensions);
        }

        public static string[] FromDataStore(string strExtesions)
        {
            return JsonConvert.DeserializeObject<string[]>(strExtesions);
        }
    }
}
