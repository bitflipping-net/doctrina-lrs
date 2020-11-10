using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Exceptions;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Doctrina.Server.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// Binds a single statement
    /// </summary>
    public class StatementModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Specify a default argument name if none is set by ModelBinderAttribute
            //var modelName = bindingContext.BinderModelName;
            //if (string.IsNullOrEmpty(modelName))
            //{
            //    modelName = "statements";
            //}

            var request = bindingContext.ActionContext.HttpContext.Request;

            try
            {
                var jsonModelReader = new JsonModelReader(request.Headers, request.Body);
                Statement statement = await jsonModelReader.ReadAs<Statement>();
                if (statement != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(statement);
                    return;
                }
            }
            catch (JsonModelReaderException ex)
            {
                bindingContext.ModelState.TryAddModelException<Statement>(a => a, ex);
            }
            bindingContext.Result = ModelBindingResult.Failed();
        }
    }
}
