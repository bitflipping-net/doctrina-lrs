using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementsCommand : IRequest<ICollection<Guid>>
    {
        public CreateStatementsCommand()
        {
            Statements = new StatementCollection();
        }

        public ICollection<Statement> Statements { get; internal set; }

        public static CreateStatementsCommand Create(StatementCollection statements)
        {
            return new CreateStatementsCommand()
            {
                Statements = statements
            };
        }
    }
}
