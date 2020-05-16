using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.WebUI.ExperienceApi.Mvc.ActionResults
{
     public static class MultipartContentExtensions
     {
         public static void AddAttachment(this MultipartContent multipart, Attachment attachment)
         {
            if (attachment.Payload == null)
            {
                throw new ArgumentException();
            }

            var byteArrayContent = new ByteArrayContent(attachment.Payload);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.ContentType);
            byteArrayContent.Headers.Add(ApiHeaders.ContentTransferEncoding, "binary");
            byteArrayContent.Headers.Add(ApiHeaders.XExperienceApiHash, attachment.SHA2);
            multipart.Add(byteArrayContent);
         }
     }
}