using System.Threading.Tasks;

namespace Doctrina.Application.Common.Caching
{
    public interface ICacheInvalidator<in TRequest>
    {
        Task Invalidate(TRequest request);
    }
}