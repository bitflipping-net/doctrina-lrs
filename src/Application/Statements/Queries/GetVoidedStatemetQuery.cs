using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class GetVoidedStatemetQuery : IRequest<Statement>
    {
        public ResultFormat Format { get; private set; }
        public Guid VoidedStatementId { get; private set; }
        public bool IncludeAttachments { get; private set; }

        public static GetVoidedStatemetQuery Create(Guid voidedStatementId, bool includeAttachments, ResultFormat format)
        {
            return new GetVoidedStatemetQuery()
            {
                VoidedStatementId = voidedStatementId,
                IncludeAttachments = includeAttachments,
                Format = format
            };
        }

        public class Handler :IRequestHandler<GetVoidedStatemetQuery, Statement>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDoctrinaDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Statement> Handle(GetVoidedStatemetQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Statements
                        .Where(x => x.StatementId == request.VoidedStatementId && x.Voided == true);

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

                return _mapper.Map<Statement>(statementEntity);
            }
        }
    }
}
