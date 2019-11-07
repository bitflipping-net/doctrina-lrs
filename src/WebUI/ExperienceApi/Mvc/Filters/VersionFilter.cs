using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Mvc.Filters
{
    /// <summary>
    /// Ensures the request has a supported 'X-Experience-Api-Version' header.
    /// </summary>
    public class RequiredVersionHeaderAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var supported = ApiVersion.GetSupported();
            try
            {
                if (!context.HttpContext.Request.Headers.ContainsKey(ApiHeaders.XExperienceApiVersion))
                    throw new Exception($"Missing '{ApiHeaders.XExperienceApiVersion}' header.");

                string requestVersion = context.HttpContext.Request.Headers[ApiHeaders.XExperienceApiVersion];
                if (string.IsNullOrEmpty(requestVersion))
                    throw new Exception($"'{ApiHeaders.XExperienceApiVersion}' header or it's null or empty.");

                try
                {
                    ApiVersion version = requestVersion;
                }
                catch (Exception)
                {
                    throw new Exception($"'{ApiHeaders.XExperienceApiVersion}' header is '{requestVersion}' which is not supported.");
                }

                await next();
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.Headers.Add(ApiHeaders.XExperienceApiVersion, ApiVersion.GetLatest().ToString());
                context.Result = new BadRequestObjectResult(ex.Message + " Supported Versions are: " + string.Join(", ", supported.Select(x => x.Key)));
            }
        }
    }
}
