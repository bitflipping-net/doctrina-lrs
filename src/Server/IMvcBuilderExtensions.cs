using Doctrina.Server.Controllers;
using Doctrina.Server.Mvc.Formatters;
using Doctrina.Server.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Server
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddExperienceApi(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(StatementsController).Assembly)
                .AddControllersAsServices();

            mvcBuilder.AddMvcOptions(options =>
            {
                options.InputFormatters.Insert(0, new RawRequestBodyFormatter());

                options.ModelBinderProviders.Insert(0, new IriModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new AgentModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new StatementModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new StatementCollectionModelBinderProvider());
            });

            return mvcBuilder;
        }
    }
}
