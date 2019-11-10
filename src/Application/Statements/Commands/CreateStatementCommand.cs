using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Commands
{
    /// <summary>
    /// Creates statement without saving to database
    /// </summary>
    public class CreateStatementCommand : IRequest<Guid>
    {
        public IStatement Statement { get; private set; }

        internal static CreateStatementCommand Create(IStatement statement)
        {
            return new CreateStatementCommand()
            {
                Statement = statement
            };
        }
    }
}
