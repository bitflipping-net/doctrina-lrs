using Doctrina.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Doctrina.WebUI.ExperienceApi.Routing
{
    public class AlternateRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] allowedMethodNames = new string[] { "POST", "GET", "PUT", "DELETE" };
        private readonly string[] formHttpHeaders = new string[] { "Authorization", "X-Experience-API-Version", "Content-Type", "Content-Length", "If-Match", "If-None-Match" };
        // TODO: This might work in most chases but is not really valid.
        private readonly Regex unsafeUrlRegex = new Regex(@"^-\]_.~!*'();:@&=+$,/?%#[A-z0-9]");

        public AlternateRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.StartsWith("/xapi/"))
            {
                AlternateRequest(context);
            }
            return _next(context);
        }

        private void AlternateRequest(HttpContext context)
        {
            var request = context.Request;
            if (request.Method.ToUpperInvariant() != "POST")
            {
                return;
            }

            // Must include parameter method
            var methodQuery = request.Query["method"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(methodQuery))
            {
                return;
            }

            // Multiple query parameters are not allowed
            if (request.Query.Count != 1)
            {
                throw new BadRequestException("An LRS will reject an alternate request syntax which contains any extra information with error code 400 Bad Request (Communication 1.3.s3.b4)");
            }

            if (!allowedMethodNames.Contains(methodQuery.ToUpperInvariant()))
            {
                return;
            }

            if(request.Method != "POST")
            {
                throw new BadRequestException("An LRS rejects an alternate request syntax not issued as a POST");
            }

            // Set request method to query method
            request.Method = methodQuery;

            if (!request.HasFormContentType)
            {
                throw new BadRequestException("Alternate request syntax sending content does not have a form parameter with the name of \"content\"");
            }

            // Ensure correct content type
            //var mediaTypeValue = MediaTypeHeaderValue.Parse(request.ContentType);
            //if (mediaTypeValue.MediaType != "application/x-www-form-urlencoded")
            //{
            //    return;
            //}
            ////else
            ////{
            ////    // Change content type
            ////    request.ContentType = "application/json";
            ////}

            // Parse form data values
            var formData = request.Form.ToDictionary(x => x.Key, y => y.Value.ToString());
            request.ContentType = "application/json";

            if (new string[] { "POST", "PUT" }.Contains(methodQuery))
            {
                if (!formData.ContainsKey("content"))
                {
                    // An LRS will reject an alternate request syntax sending content which does not have a form parameter with the name of \"content\" (Communication 1.3.s3.b4)
                    context.Response.StatusCode = 400;
                    throw new BadRequestException("Alternate request syntax sending content does not have a form parameter with the name of \"content\"");
                }

                // Content-Type form header is not required
                //if (!formData.Any(x=> x.Key.Equals("Content-Type", StringComparison.InvariantCultureIgnoreCase)))
                //{
                //    context.Response.StatusCode = 400;
                //    throw new Exception("Alternate request syntax sending content does not have a form header parameter with the name of \"Content-Type\"");
                //}
            }

            if (formData.ContainsKey("content"))
            {
                string urlEncodedContent = formData["content"];

                if (unsafeUrlRegex.IsMatch(urlEncodedContent))
                {
                    throw new Exception($"Form data 'content' contains unsafe charactors.");
                }

                string decodedContent = HttpUtility.UrlDecode(urlEncodedContent);
                var ms = new MemoryStream();
                using(var sw = new StreamWriter(ms, Encoding.UTF8, leaveOpen: true))
                {
                    sw.Write(decodedContent);
                    //sw.Flush();
                    //sw.Close();
                }
                ms.Position = 0;
                request.Body = ms;

                formData.Remove("content");
            }

            // Treat all known form headers as request http headers
            if (formData.Any())
            {
                foreach (var headerName in formHttpHeaders)
                {
                    var formHeader = formData.FirstOrDefault(x => x.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase));
                    if (!formHeader.Equals(default(KeyValuePair<string, string>)))
                    {
                        request.Headers[formHeader.Key] = formHeader.Value;
                        formData.Remove(formHeader.Key);
                    }
                }
            }

            // Treat the rest as query parameters
            var queryCollection = HttpUtility.ParseQueryString(string.Empty);
            foreach (var name in formData)
            {
                queryCollection.Add(name.Key, name.Value);
            }
            if (queryCollection.Count > 0)
            {
                request.QueryString = new QueryString("?" + queryCollection.ToString());
            }
            else
            {
                request.QueryString = new QueryString();
            }
        }
    }
}
