using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Infrastructure
{
    public static class HttpResponseExtensions
    {
        public static Task WriteJsonAsync(this HttpResponse response, object json, string contentType = null, CancellationToken cancellationToken = default)
        {
            string jsonString = JsonConvert.SerializeObject(json);
            return response.WriteAsync(jsonString, cancellationToken);
        }

        public static Task WriteJsonAsync(this HttpResponse response, string json, string contentType = null, CancellationToken cancellationToken = default)
        {
            return response.WriteAsync(json, cancellationToken);
        }
    }
}
