using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding
{
    public class AgentModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var agent = new Agent(valueProviderResult.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(agent);
            }
            catch (System.Exception)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
