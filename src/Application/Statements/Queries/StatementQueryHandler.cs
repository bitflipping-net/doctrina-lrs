using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.Statements.Queries
{
    public class StatementQueryHandler : IRequestHandler<StatementQuery, StatementEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public StatementQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StatementEntity> Handle(StatementQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Statements
                    .AsNoTracking()
                    .Where(x => x.StatementId == request.StatementId
                        && x.VoidingStatementId == null);

            if (request.IncludeAttachments)
            {
                query = query
                    .Include(x => x.Attachments)
                    .Select(x => new StatementEntity()
                    {
                        StatementId = x.StatementId,
                        FullStatement = x.FullStatement,
                        Attachments = x.Attachments
                    });
            }
            else
            {
                query = query.Select(x => new StatementEntity()
                {
                    StatementId = x.StatementId,
                    FullStatement = x.FullStatement
                });
            }

            StatementEntity statementEntity = await query.FirstOrDefaultAsync(cancellationToken);

            if (statementEntity == null)
            {
                return null;
            }

            return statementEntity;
        }
    }
}