using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Doctrina.Server.Mvc.ModelBinding.Binders
{
    public class StatementModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.IsComplexType)
            {
                return null;
            }

            // string propName = context.Metadata.ParameterName;
            // if (propName == null)
            // {
            //     return null;
            // }

            var modelType = context.Metadata.ModelType;
            if (modelType == null)
            {
                return null;
            }

            if (modelType == typeof(Statement))
            {
                return new BinderTypeModelBinder(typeof(StatementModelBinder));
            }

            return null;
        }
    }
}
