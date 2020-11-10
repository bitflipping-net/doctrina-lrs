using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Clients.Queries
{
    public class ClientByAgentQueryHandler : IRequestHandler<ClientByAgentQuery, Client>
    {
        private readonly IDoctrinaDbContext _dbContext;

        public ClientByAgentQueryHandler(IDoctrinaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Handle(ClientByAgentQuery request, CancellationToken cancellationToken)
        {
            string strAuthority = request.Agent.ToJson();

            Client client = await _dbContext.Clients
                .SingleOrDefaultAsync(cl => cl.Authority == strAuthority);

            return client;
        }
    }
}
