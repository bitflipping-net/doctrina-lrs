using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class GetConsistentThroughQuery : IRequest<DateTimeOffset?>
    {
        public class Handler : IRequestHandler<GetConsistentThroughQuery, DateTimeOffset?>
        {
            private readonly IDoctrinaDbContext _context;

            public Handler(IDoctrinaDbContext context)
            {
                _context = context;
            }

            public async Task<DateTimeOffset?> Handle(GetConsistentThroughQuery request, CancellationToken cancellationToken)
            {
                var first = await _context.Statements.OrderByDescending(x => x.Stored)
                    .FirstOrDefaultAsync(cancellationToken);
                return first?.Stored;
            }
        }
    }
}
