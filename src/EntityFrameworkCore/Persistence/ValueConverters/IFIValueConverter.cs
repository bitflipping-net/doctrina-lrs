using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class IFITypeValueConverter : ValueConverter<Ifi, string>
    {
        public IFITypeValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<Ifi, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, Ifi>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(Ifi ifi)
        {
            return ifi.ToString();
        }

        public static Ifi FromDataStore(string s)
        {
            return Ifi.Parse(s);
        }
    }
}
