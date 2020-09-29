using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Doctrina.Persistence.Infrastructure
{
    public class StoreColumnIntercepter : DbCommandInterceptor
    {
        private readonly IStoreHttpContext _storeContext;

        public StoreColumnIntercepter(IStoreHttpContext storeContext)
        {
            _storeContext = storeContext;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            //if (!command.CommandText.Contains("StoreId"))
            //{
            //    if (command.CommandText.Contains("WHERE"))
            //    {
            //        command.CommandText += $" AND StoreId = '{_storeContext.GetStoreId()}'";
            //    }
            //    else
            //    {
            //        command.CommandText += $" WHERE StoreId = '{_storeContext.GetStoreId()}'";
            //    }
            //}

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}
