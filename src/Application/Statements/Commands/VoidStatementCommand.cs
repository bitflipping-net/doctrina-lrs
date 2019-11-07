using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class VoidStatementCommand : IRequest
    {
        public IStatementBaseEntity Statement { get; set; }

        public class Handler : IRequestHandler<VoidStatementCommand>
        {
            private IDoctrinaDbContext _context;

            public Handler(IDoctrinaDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(VoidStatementCommand request, CancellationToken cancellationToken)
            {
                IStatementBaseEntity voidingStatement = request.Statement;

                StatementRefEntity statementRef = voidingStatement.Object.StatementRef as StatementRefEntity;
                Guid? statementRefId = statementRef.Id;

                // Fetch statement to be voided
                StatementEntity voidedStatement = await _context.Statements
                    .FirstOrDefaultAsync(x => x.StatementId == statementRefId, cancellationToken);

                // Upon receiving a Statement that voids another, the LRS SHOULD NOT* reject the request on the grounds of the Object of that voiding Statement not being present.
                if (voidedStatement == null)
                {
                    await Task.CompletedTask; // Soft
                }

                // Any Statement that voids another cannot itself be voided.
                if (voidedStatement.Verb.Id == ExperienceApi.Data.Verbs.Voided)
                {
                    await Task.CompletedTask; // Soft
                }

                // voidedStatement has been voided, return.
                if (voidedStatement.Voided)
                {
                    await Task.CompletedTask; // Soft
                }

                voidedStatement.Voided = true;

                _context.Statements.Update(voidedStatement);

                return await Unit.Task;
            }
        }
    }
}
