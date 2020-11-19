using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace Doctrina.Application.Identity
{
    public class ClientContext : IClientContext
    {
        private Client _client;
        public Client Client
        {
            get
            {
                if (_client == null)
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    if (request.Headers.TryGetValue("Authorization", out StringValues authHeader))
                    {
                        string api = authHeader.ToString().Substring("Basic ".Length);
                        _client = _dbContext.Clients.SingleOrDefault(x => x.API == api);
                    }
                }

                return _client;
            }
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDoctrinaDbContext _dbContext;

        public ClientContext(IHttpContextAccessor httpContextAccessor, IDoctrinaDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public Agent GetClientAuthority()
        {
            return new Agent(Client.Authority);
        }

        public Guid GetClientId()
        {
            return Client.ClientId;
        }
    }
}
