using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Commands
{
    /// <summary>
    /// Prepare XAPI Statement for creation
    /// </summary>
    public class CreateStatementCommand : IRequest<Guid>
    {
        public Statement Statement { get; private set; }

        internal static CreateStatementCommand Create(Statement statement)
        {
            return new CreateStatementCommand()
            {
                Statement = statement
            };
        }
    }
}
