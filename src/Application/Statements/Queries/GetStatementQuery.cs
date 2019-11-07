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
    public class GetStatementQuery : IRequest<Statement>
    {
        public Guid StatementId { get; set; }
        public bool IncludeAttachments { get; set; }
        public ResultFormat Format { get; set; }

        public static GetStatementQuery Create(Guid statementId, bool includeAttachments = false, ResultFormat format = ResultFormat.Exact)
        {
            return new GetStatementQuery()
            {
                StatementId = statementId,
                IncludeAttachments = includeAttachments,
                Format = format
            };
        }

        public class Handler : IRequestHandler<GetStatementQuery, Statement>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDoctrinaDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Statement> Handle(GetStatementQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Statements
                        .Where(x => x.StatementId == request.StatementId && x.Voided == false);

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
