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
            var stringContent = new StringContent(result.ToJson(format), Encoding.UTF8, MediaTypes.Application.Json);

            var attachmentsWithPayload = result.Statements.SelectMany(x => x.Attachments.Where(a => a.Payload != null));
            if (attachmentsWithPayload.Count() > 0)
            {
                string boundary = Guid.NewGuid().ToString();
                using var multipart = new MultipartContent("mixed", boundary)
                {
                    stringContent
                };

                foreach (var attachment in attachmentsWithPayload)
                {
                    var byteArrayContent = new ByteArrayContent(attachment.Payload);
                    var attachmentMediaType = MediaTypeHeaderValue.Parse(attachment.ContentType);

                    byteArrayContent.Headers.ContentType = attachmentMediaType;
                    byteArrayContent.Headers.Add(ApiHeaders.ContentTransferEncoding, "binary");
                    byteArrayContent.Headers.Add(ApiHeaders.XExperienceApiHash, attachment.SHA2);
                    multipart.Add(byteArrayContent);
                }

                // Write Content-Type header with Boundary parameter
                var mediaType = MediaTypeHeaderValue.Parse(MediaTypes.Multipart.Mixed);
                mediaType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));
                context.HttpContext.Response.ContentType = mediaType.ToString();
                
                await multipart.CopyToAsync(context.HttpContext.Response.Body);
            }

            context.HttpContext.Response.ContentType = MediaTypes.Application.Json;
            await stringContent.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}
