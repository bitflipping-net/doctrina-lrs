using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueConverters
{
    public class AccountValueConverter : ValueConverter<Account, string>
    {
        public AccountValueConverter()
           : base(convertToProviderExpression, convertFromProviderExpression)
        {
        }

        private static readonly Expression<Func<Account, string>> convertToProviderExpression = e => ToDataStore(e);
        private static readonly Expression<Func<string, Account>> convertFromProviderExpression = e => FromDataStore(e);

        private static string ToDataStore(Account e)
        {
            return string.Join('@', e.Name, e.HomePage);
        }
        private static Account FromDataStore(string accountString)
        {
            var parts = accountString.Split('@');

            return 
        }
    }
}
