using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doctrina.WebUI.ExperienceApi.Mvc.Filters
{
    /// <summary>
    /// Accepts HEAD requests without body
    /// </summary>
    public class HeadWithoutBody : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == "HEAD")
            {
                int statusCode = context.HttpContext.Response.StatusCode;
                if (statusCode == 200
                    || statusCode == 204)
                {
                    // Return No Content
                    context.Result = new StatusCodeResult(statusCode);
                }
            }
        }
    }
}
