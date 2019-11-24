using Doctrina.Application.Common.Exceptions;
using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Exceptions;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
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

            //string strContentType = request.ContentType ?? MediaTypes.Application.Json;

            //try
            //{
            //    var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(strContentType);
            //}
            //catch (FormatException ex)
            //{
            //    throw new BadRequestException(ex.InnerException?.Message ?? ex.Message, ex);
            //}

            try
            {
                var jsonModelReader = new JsonModelReader(request.Headers, request.Body);
                model.Statements = await jsonModelReader.ReadAs<StatementCollection>();
            }
            catch (JsonModelReaderException ex)
            {
                throw new BadRequestException(ex.InnerException?.Message ?? ex.Message, ex);
            }

            if (model.Statements == null)
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
