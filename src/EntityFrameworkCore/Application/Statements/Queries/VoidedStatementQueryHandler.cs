using AutoMapper;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Doctrina.Persistence.Infrastructure;
using System.Threading.Tasks;
using Doctrina.Application.Statements.Queries;

namespace Doctrina.Application.Statements.Queries
{
    public class VoidedStatemetQueryHandler : IRequestHandler<VoidedStatemetQuery, StatementEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public VoidedStatemetQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StatementEntity> Handle(VoidedStatemetQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Statements
                    .Where(x => x.StatementId == request.VoidedStatementId
                        && x.VoidingStatementId != null);

            if (request.IncludeAttachments)
            {
                query = query.Include(x => x.Attachments)
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
