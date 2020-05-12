using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc;
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

        public Task ExecuteResultAsync(ActionContext context)
        {
            string strJson = _statement.ToJson(_format);
            var stringContent = new StringContent(strJson, Encoding.UTF8, MediaTypes.Application.Json);

            if (_statement.Attachments.Any(x => x.Payload != null))
            {
                var multipart = new MultipartContent("mixed")
                {
                    stringContent
                };
                foreach (var attachment in _statement.Attachments)
                {
                    if (attachment.Payload != null)
                    {
                        var byteArrayContent = new ByteArrayContent(attachment.Payload);
                        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.ContentType);
                        byteArrayContent.Headers.Add(ApiHeaders.ContentTransferEncoding, "binary");
                        byteArrayContent.Headers.Add(ApiHeaders.XExperienceApiHash, attachment.SHA2);
                        multipart.Add(byteArrayContent);
                    }
                }

                return multipart.CopyToAsync(context.HttpContext.Response.Body);
            }

            return stringContent.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}
