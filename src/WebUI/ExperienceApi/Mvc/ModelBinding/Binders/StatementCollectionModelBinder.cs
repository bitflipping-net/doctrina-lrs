using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Exceptions;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding
{
    public class StatementCollectionModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(StatementCollection))
            {
                return;
            }

            try
            {
                var request = bindingContext.ActionContext.HttpContext.Request;
                var jsonModelReader = new JsonModelReader(request.Headers, request.Body);

                var statements = await jsonModelReader.ReadAs<StatementCollection>();

                bindingContext.Result = ModelBindingResult.Success(statements);
                return;
            }
            catch (JsonModelReaderException ex)
            {
                bindingContext.ModelState.TryAddModelException<StatementCollection>(x=> x, ex);
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}
