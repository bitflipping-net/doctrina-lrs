using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Commands
{
    public class PutStatementCommand : IRequest
    {
        public Guid StatementId { get; private set; }
        public Statement Statement { get; private set; }

        public static PutStatementCommand Create(Guid statementId, Statement statement)
        {
            return new PutStatementCommand()
            {
                StatementId = statementId,
                Statement = statement
            };
        }
    }
}
