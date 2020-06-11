using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.SubStatements.Commands
{
    public class CreateSubStatementCommand : IRequest<SubStatementEntity>
    {
        public SubStatement SubStatement { get; private set; }

        internal static CreateSubStatementCommand Create(SubStatement subStatement)
        {
            return new CreateSubStatementCommand()
            {
                SubStatement = subStatement
            };
        }
    }
}
