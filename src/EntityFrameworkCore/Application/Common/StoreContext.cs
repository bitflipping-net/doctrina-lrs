using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common
{
    internal class StoreContext : IStoreContext
    {
        private readonly IDoctrinaDbContext _dbContext;
        private Client _client;
        private Store _store;

        public StoreContext(IDoctrinaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Store> AuthorizeAsync(Client client, CancellationToken cancellationToken = default)
        {
            _client = client;

            _store = await _dbContext.Stores.SingleOrDefaultAsync(store => 
                store.Clients.Any(c => c.ClientId == client.ClientId), 
                cancellationToken
            );

            return _store;
        }

        public Store GetStore()
        {
            if(_store == null)
            {
                throw new NullReferenceException("Store has not been authorized by a client");
            }

            return _store;
        }

        public Guid GetStoreId()
        {
            if (_store == null)
            {
                throw new NullReferenceException("Store has not been authorized by a client");
            }

            return _store.StoreId;
        }

        public string GetClientAuthority()
        {
            if (_store == null)
            {
                throw new NullReferenceException("Store has not been authorized by a client");
            }

            return _client.Authority;
        }
    }
}
