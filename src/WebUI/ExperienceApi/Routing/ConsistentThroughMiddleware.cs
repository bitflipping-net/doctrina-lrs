using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Client.Http;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Routing
{
    public class ConsistentThroughMiddleware
    {
        private readonly RequestDelegate _next;

        public ConsistentThroughMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDoctrinaAppContext appContext)
        {
            // Execute next
            await _next(context);

            if (context.Request.Path.HasValue && context.Request.Path.StartsWithSegments("/xapi"))
            {
                string headerKey = ApiHeaders.XExperienceApiConsistentThrough;
                var headers = context.Response.Headers;

                if (!headers.ContainsKey(headerKey))
                {
                    if (!headers.ContainsKey(headerKey))
                    {
                        headers.Add(headerKey, appContext.ConsistentThroughDate.ToString("o"));
                    }
                }
            }
        }
    }
}
