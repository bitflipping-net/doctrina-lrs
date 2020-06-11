using AutoMapper;
using Doctrina.Application.Common.Interfaces;
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
    public class VoidedStatemetQuery : IRequest<StatementEntity>
    {
        public ResultFormat Format { get; private set; }
        public Guid VoidedStatementId { get; private set; }
        public bool IncludeAttachments { get; private set; }

        public static VoidedStatemetQuery Create(Guid voidedStatementId, bool includeAttachments, ResultFormat format)
        {
            return new VoidedStatemetQuery()
            {
                VoidedStatementId = voidedStatementId,
                IncludeAttachments = includeAttachments,
                Format = format
            };
        }

        public class Handler : IRequestHandler<VoidedStatemetQuery, StatementEntity>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDoctrinaDbContext context, IMapper mapper)
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
}
