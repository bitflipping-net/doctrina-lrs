using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentIdQueryHandler : IRequestHandler<GetAgentIdQuery, Guid?>
    {
        private readonly IDoctrinaDbContext _context;
        private IMediator _mediator;

        public GetAgentIdQueryHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid?> Handle(GetAgentIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(GetAgentQuery.Create(request.Agent));
            return agent?.AgentId;
        }
    }
}