using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Persistence.ValueConverters
{
    public class ContextActivityCollectionValueConverter : ValueConverter<ContextActivityCollection, string>
    {
        public ContextActivityCollectionValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<ContextActivityCollection, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, ContextActivityCollection>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(ContextActivityCollection extensions)
        {
            return JsonConvert.SerializeObject(extensions);
        }

        public static ContextActivityCollection FromDataStore(string strExtesions)
        {
            return JsonConvert.DeserializeObject<ContextActivityCollection>(strExtesions);
        }
    }
}
