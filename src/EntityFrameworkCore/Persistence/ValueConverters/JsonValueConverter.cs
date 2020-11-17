using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Doctrina.Persistence.ValueConverters
{
    public class JsonValueConverter<TObject> : ValueConverter<TObject, string>
    {
        public JsonValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<TObject, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, TObject>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(TObject extensions)
        {
            return JsonConvert.SerializeObject(extensions);
        }

        public static TObject FromDataStore(string strExtesions)
        {
            return JsonConvert.DeserializeObject<TObject>(strExtesions);
        }
    }
}
