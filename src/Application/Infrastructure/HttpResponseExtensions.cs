using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Infrastructure
{
    public static class HttpResponseExtensions
    {
        public static Task WriteJsonAsync(this HttpResponse httpResponse, object json, CancellationToken cancellationToken = default)
        {
            string jsonString = JsonConvert.SerializeObject(json);
            return httpResponse.WriteAsync(jsonString, cancellationToken);
        }
    }
}
