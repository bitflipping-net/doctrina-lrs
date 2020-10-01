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
    public class VoidedStatemetQueryHandler : IRequestHandler<VoidedStatemetQuery, StatementModel>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public VoidedStatemetQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StatementModel> Handle(VoidedStatemetQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Statements
                    .Where(x => x.StatementId == request.VoidedStatementId
                        && x.VoidingStatementId != null);

            if (request.IncludeAttachments)
            {
                query = query.Include(x => x.Attachments)
                    .Select(x => new StatementModel()
                    {
                        StatementId = x.StatementId,
                        Encoded = x.Encoded,
                        Attachments = x.Attachments
                    });
            }
            else
            {
                query = query.Select(x => new StatementModel()
                {
                    StatementId = x.StatementId,
                    Encoded = x.Encoded
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
