using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Exceptions;
using Doctrina.WebUI.ExperienceApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding
{
    public class PostStatementsModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(PostStatementContent))
            {
                return;
            }

            var model = new PostStatementContent();

            var request = bindingContext.ActionContext.HttpContext.Request;

            string strContentType = request.ContentType ?? MediaTypes.Application.Json;
            // TODO: Should we assume application/json?
            //if (string.IsNullOrEmpty(strContentType))
            //{
            //    bindingContext.Result = ModelBindingResult.Failed();
            //    return;
            //}

            var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(strContentType);

            try
            {
                var jsonModelReader = new JsonModelReader(mediaTypeHeaderValue, request.Body);
                model.Statements = await jsonModelReader.ReadAs<StatementCollection>();
            }
            catch (JsonModelReaderException ex)
            {
                throw new BadRequestException(ex.Message);
            }

            if(model.Statements == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(model);
            }
        }
    }
}
