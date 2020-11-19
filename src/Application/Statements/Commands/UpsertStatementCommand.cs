using Doctrina.Domain.Entities.Interfaces;
using MediatR;

namespace Doctrina.Application.Statements.Commands
{
    public class UpsertStatementCommand : IRequest<IStatementBaseEntity>
    {
        public IStatementBaseEntity Statement { get; private set; }

        public static UpsertStatementCommand Create(IStatementBaseEntity statement)
        {
            return new UpsertStatementCommand()
            {
                Statement = statement
            };
        }
    }
}