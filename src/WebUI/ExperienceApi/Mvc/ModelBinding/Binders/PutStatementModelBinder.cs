using Doctrina.ExperienceApi;
using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Doctrina.WebUI.Mvc.ModelBinders
{
    /// <summary>
    /// Binds a single statement
    /// </summary>
    public class PutStatementModelBinder : IModelBinder
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

            var contentType = MediaTypeHeaderValue.Parse(request.ContentType);

            try
            {
                var jsonModelReader = new JsonModelReader(contentType, request.Body);
                Statement statement = await jsonModelReader.ReadAs<Statement>();
                bindingContext.Result = ModelBindingResult.Success(statement);
            }
            catch (JsonModelReaderException ex)
            {
                throw new BadRequestException(ex.Message);
            }

            bindingContext.Result = ModelBindingResult.Failed();
        }
    }
}
