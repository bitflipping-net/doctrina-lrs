using Doctrina.WebUI.ExperienceApi.Controllers;
using Doctrina.WebUI.ExperienceApi.Mvc.Formatters;
using Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.WebUI.ExperienceApi
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
            });

            return mvcBuilder;
        }
    }
}
