using Doctrina.Application.Identity;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Http;
using System;

namespace Doctrina.WebUI
{
    public class ClientContext : IClientContext
    {
        private readonly Guid _clientId = new Guid("4C9C8905-0A58-40A3-AD18-D6C81C4DCE9E");

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Agent GetClientAuthority()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string scheme = request.Scheme;
            string host = request.Host.ToString();

            return new Agent()
            {
                Account = new Account()
                {
                    HomePage = new Uri($"{scheme}://{host}"),
                    Name = "Doctrina.WebUI"
                }
            };
        }

        public Guid GetClientId()
        {
            return _clientId;
        }
    }
}
