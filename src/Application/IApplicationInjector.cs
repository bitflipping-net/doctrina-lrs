using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Application
{
    public interface IApplicationInjector
    {
        IServiceCollection AddApplication(IServiceCollection services, IConfiguration config);
    }
}