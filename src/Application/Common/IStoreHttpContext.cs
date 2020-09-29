using System;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.Domain.Models;

namespace Doctrina.Application.Common.Interfaces
{
    public interface IStoreHttpContext
    {
        Task<Store> AuthorizeAsync(Client client, CancellationToken cancellationToken = default);

        Guid GetStoreId();

        string GetClientAuthority();
    }
}
