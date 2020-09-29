using AutoMapper;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class StatementQueryHandler : IRequestHandler<StatementQuery, StatementModel>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public StatementQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StatementModel> Handle(StatementQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Statements
                    .AsNoTracking()
                    .Where(x => x.StatementId == request.StatementId
                        && x.VoidingStatementId == null);

            if (request.IncludeAttachments)
            {
                query = query
                    .Include(x => x.Attachments)
                    .Select(x => new StatementModel()
                    {
                        StatementId = x.StatementId,
                        FullStatement = x.FullStatement,
                        Attachments = x.Attachments
                    });
            }
            else
            {
                query = query.Select(x => new StatementModel()
                {
                    StatementId = x.StatementId,
                    FullStatement = x.FullStatement
                });
            }

            StatementModel statementEntity = await query.FirstOrDefaultAsync(cancellationToken);

            if (statementEntity == null)
            {
                return null;
            }

            return statementEntity;
        }
    }
}