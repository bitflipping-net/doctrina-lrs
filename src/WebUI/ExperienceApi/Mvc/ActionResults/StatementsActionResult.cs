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
    public class StatementsActionResult : IActionResult
    {
        private readonly StatementsResult _result;
        private readonly ResultFormat _format;
        private readonly bool _attachments;

        public StatementsActionResult(StatementsResult result, ResultFormat format, bool attachments)
        {
            _result = result;
            _format = format;
            _attachments = attachments;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            HttpContent httpContent = new StringContent(_result.ToJson(_format), Encoding.UTF8, MediaTypes.Application.Json);

            if (_attachments)
            {
                string boundary = Guid.NewGuid().ToString();
                httpContent = new MultipartContent("mixed", boundary)
                {
                    httpContent
                };

                var attachmentsWithPayload = _result.Statements.SelectMany(x => x.Attachments.Where(a => a.Payload != null));
                foreach (var attachment in attachmentsWithPayload)
                {
                    ((MultipartContent)httpContent).AddAttachment(attachment);
                }
            }

            foreach(var header in httpContent.Headers)
            {
                context.HttpContext.Response.Headers.Add(
                    header.Key,
                    new StringValues(header.Value.ToArray())
                );
            }

            await httpContent.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}
