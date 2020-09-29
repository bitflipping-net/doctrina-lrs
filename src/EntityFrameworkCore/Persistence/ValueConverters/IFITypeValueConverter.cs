using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class IFITypeValueConverter : ValueConverter<InverseFunctionalIdentifier, string>
    {
        public IFITypeValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<InverseFunctionalIdentifier, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, InverseFunctionalIdentifier>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(InverseFunctionalIdentifier extensions)
        {
            return extensions.ToString().ToLowerInvariant();
        }

        public static InverseFunctionalIdentifier FromDataStore(string s)
        {
            return (InverseFunctionalIdentifier)Enum.Parse(typeof(InverseFunctionalIdentifier), s);
        }
    }
}
