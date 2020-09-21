using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class IFITypeValueConverter : ValueConverter<IFIType, string>
    {
        public IFITypeValueConverter(ConverterMappingHints mappingHints = null)
            : base(covertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static readonly Expression<Func<IFIType, string>> covertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, IFIType>> convertFromProviderExpression = e => FromDataStore(e);

        public static string ToDataStore(IFIType extensions)
        {
            return extensions.ToString().ToLowerInvariant();
        }

        public static IFIType FromDataStore(string s)
        {
            return (IFIType)Enum.Parse(typeof(IFIType), s);
        }
    }
}
