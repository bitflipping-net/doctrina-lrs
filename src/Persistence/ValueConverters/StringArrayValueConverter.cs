using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class StringArrayValueConverter : ValueConverter<ICollection<string>, string>
    {
        public StringArrayValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<ICollection<string>, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, ICollection<string>>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(ICollection<string> extensions)
        {
            return JsonConvert.SerializeObject(extensions);
        }

        public static ICollection<string> FromDataStore(string strExtesions)
        {
            return JsonConvert.DeserializeObject<ICollection<string>>(strExtesions);
        }
    }
}
