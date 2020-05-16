using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ActionResults
{
    public class StatementActionResult : IActionResult
    {
        private readonly Statement _statement;
        private readonly ResultFormat _format;

        public StatementActionResult(Statement statement, ResultFormat format)
        {
            _statement = statement;
            _format = format;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            string strJson = _statement.ToJson(_format);
            HttpContent httpContent = new StringContent(strJson, Encoding.UTF8, MediaTypes.Application.Json);

            var attachmentsWithPayload = _statement.Attachments.Where(a => a.Payload != null);
            if (_statement.Attachments.Any(x => x.Payload != null))
            {
                string boundary = Guid.NewGuid().ToString();
                httpContent = new MultipartContent("mixed", boundary)
                {
                    httpContent
                };

                foreach (var attachment in _statement.Attachments)
                {
                    if (attachment.Payload != null)
                    {
                        ((MultipartContent)httpContent).AddAttachment(attachment);
                    }
                }
            }

            foreach(var header in httpContent.Headers)
            {
                context.HttpContext.Response.Headers.Add(
                    header.Key,
                    new StringValues(header.Value.ToArray())
                );
            }

            context.HttpContext.Response.Headers.Add(ApiHeaders.XExperienceApiVersion, ApiVersion.GetLatest().ToString());
            await httpContent.CopyToAsync(context.HttpContext.Response.Body);

            httpContent.Dispose();
        }
    }
}
