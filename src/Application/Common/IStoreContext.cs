using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common.Interfaces
{
    public interface IStoreContext
    {
        Task<Store> AuthorizeAsync(Client client, CancellationToken cancellationToken = default);

        Guid GetStoreId();

        Store GetStore();

        Agent GetClientAuthority();
    }
}
