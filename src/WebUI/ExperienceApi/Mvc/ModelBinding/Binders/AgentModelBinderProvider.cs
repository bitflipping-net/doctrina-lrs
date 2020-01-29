using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding.Binders
{
    public class AgentModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.IsComplexType)
            {
                return null;
            }

            string propName = context.Metadata.ParameterName;
            if (propName == null)
            {
                return null;
            }

            var modelType = context.Metadata.ModelType;
            if (modelType == null)
            {
                return null;
            }

            if (modelType == typeof(Agent))
            {
                return new BinderTypeModelBinder(typeof(AgentModelBinder));
            }

            return null;
        }
    }
}
