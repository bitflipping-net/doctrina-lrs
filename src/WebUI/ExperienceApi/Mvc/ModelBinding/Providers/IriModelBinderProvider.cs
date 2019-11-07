using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding.Providers
{
    public class IriModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.IsComplexType) return null;

            var propName = context.Metadata.PropertyName;
            if (propName == null) return null;

            var modelType = context.Metadata.ModelType;
            if (modelType == null) return null;

            if (modelType != typeof(Iri))
                return null;

            return new BinderTypeModelBinder(typeof(IriModelBinder));
        }
    }
}
