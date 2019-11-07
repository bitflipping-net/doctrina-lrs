using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Domain.Entities.OwnedTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class InteractionComponentCollectionValueConverter : ValueConverter<InteractionComponentCollection, string>
    {
        public InteractionComponentCollectionValueConverter()
            : base(convertToProviderExpression, convertFromProviderExpression)
        {
        }

        private static readonly Expression<Func<InteractionComponentCollection, string>> convertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, InteractionComponentCollection>> convertFromProviderExpression = e => FromDataStore(e);

        private static string ToDataStore(InteractionComponentCollection e)
        {
            return JsonConvert.SerializeObject(e);
        }


        private static InteractionComponentCollection FromDataStore(string e)
        {
            return JsonConvert.DeserializeObject<InteractionComponentCollection>(e);
        }
    }
}
