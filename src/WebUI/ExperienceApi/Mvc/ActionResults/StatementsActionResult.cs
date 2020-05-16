using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc;
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
        private readonly StatementsResult result;
        private readonly ResultFormat format;

        public StatementsActionResult(StatementsResult result, ResultFormat format)
        {
            this.result = result;
            this.format = format;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            HttpContent httpContent = new StringContent(result.ToJson(format), Encoding.UTF8, MediaTypes.Application.Json);

            var attachmentsWithPayload = result.Statements.SelectMany(x => x.Attachments.Where(a => a.Payload != null));
            if (attachmentsWithPayload.Count() > 0)
            {
                string boundary = Guid.NewGuid().ToString();
                httpContent = new MultipartContent("mixed", boundary)
                {
                    httpContent
                };

                foreach (var attachment in attachmentsWithPayload)
                {
                    ((MultipartContent)httpContent).AddAttachment(attachment);
                }
            }

            foreach(var header in httpContent.Headers)
            {
                context.HttpContext.Response.Headers.Add(header.Key, header.Value.ToString());
            }

            context.HttpContext.Response.ContentType = MediaTypes.Application.Json;
            await httpContent.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}
