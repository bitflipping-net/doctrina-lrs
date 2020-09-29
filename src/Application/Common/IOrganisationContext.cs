using Doctrina.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common
{
    public interface IOrganisationContext
    {
        Task SetupStoreAsync(Store store, CancellationToken cancellationToken = default);
    }
}
