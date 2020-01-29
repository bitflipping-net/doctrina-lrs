using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Exceptions;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

            try
            {
                var request = bindingContext.ActionContext.HttpContext.Request;
                var jsonModelReader = new JsonModelReader(request.Headers, request.Body);

                var model = new PostStatementContent
                {
                    Statements = await jsonModelReader.ReadAs<StatementCollection>()
                };

                bindingContext.Result = ModelBindingResult.Success(model);
                return;
            }
            catch (JsonModelReaderException ex)
            {
                bindingContext.ModelState.TryAddModelException<PostStatementContent>(x=> x, ex);
            }

            bindingContext.Result = ModelBindingResult.Failed();
        }
    }
}
