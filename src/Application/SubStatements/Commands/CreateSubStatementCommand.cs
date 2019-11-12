using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.SubStatements.Commands
{
    public class CreateSubStatementCommand : IRequest<ISubStatementEntity>
    {
        public ISubStatement SubStatement { get; private set; }

        internal static CreateSubStatementCommand Create(ISubStatement subStatement)
        {
            return new CreateSubStatementCommand()
            {
                SubStatement = subStatement
            };
        }
    }
}
