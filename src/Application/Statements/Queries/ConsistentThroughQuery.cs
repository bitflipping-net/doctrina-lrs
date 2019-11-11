using Doctrina.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class ConsistentThroughQuery : IRequest<DateTimeOffset>
    {
        public class Handler : IRequestHandler<ConsistentThroughQuery, DateTimeOffset>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IDoctrinaAppContext _appContext;

            public Handler(IDoctrinaDbContext context, IDoctrinaAppContext appContext)
            {
                _context = context;
                _appContext = appContext;
            }

            public async Task<DateTimeOffset> Handle(ConsistentThroughQuery request, CancellationToken cancellationToken)
            {
                if (!_appContext.ConsistentThroughDate.HasValue)
                {
                    var first = await _context.Statements.OrderByDescending(x => x.Stored)
                        .FirstOrDefaultAsync(cancellationToken);
                    _appContext.ConsistentThroughDate = first?.Stored ?? DateTimeOffset.UtcNow;
                }
                return _appContext.ConsistentThroughDate.Value;
            }
        }
    }
}
