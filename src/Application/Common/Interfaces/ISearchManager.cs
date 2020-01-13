using Elasticsearch.Net;
using Nest;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common.Interfaces
{
    public interface ISearchManager
    {
        ElasticLowLevelClient Client { get; }

        Task RebuildIndexesAsync(CancellationToken cancellationToken = default);
    }
}