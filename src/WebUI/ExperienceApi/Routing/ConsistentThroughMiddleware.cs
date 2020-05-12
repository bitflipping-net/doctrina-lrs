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

        public async Task InvokeAsync(HttpContext context, IMediator mediator)
        {
            if (context.Request.Path.HasValue && context.Request.Path.StartsWithSegments("/xapi"))
            {
                string headerKey = ApiHeaders.XExperienceApiConsistentThrough;
                var headers = context.Response.Headers;

                if (!headers.ContainsKey(headerKey))
                {
                    if (!headers.ContainsKey(headerKey))
                    {
                        //var consistentThroughDate = await mediator.Send(new ConsistentThroughQuery());
                        headers.Add(headerKey, DateTimeOffset.Now.ToString("o"));
                    }
                }
            }

            // Execute next
            await _next(context);

            // We cannot modify response headers after
        }
    }
}
