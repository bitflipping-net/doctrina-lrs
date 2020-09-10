using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Application
{
    public interface IDataProviderInjector
    {
        IServiceCollection AddPersistence(IServiceCollection services, IConfiguration config);
        IServiceCollection AddApplication(IServiceCollection services, IConfiguration config);
    }
}