using AutoMapper;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.SubStatements
{
    public class CreateSubStatementCommandHandler : IRequestHandler<CreateSubStatementCommand, SubStatementEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSubStatementCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<SubStatementEntity> Handle(CreateSubStatementCommand request, CancellationToken cancellationToken)
        {
            var subStatement = _mapper.Map<SubStatementEntity>(request.SubStatement);

            await _mediator.Send(MergeVerbCommand.Create(subStatement.Verb));

            await _mediator.Send(MergeActorCommand.Create(subStatement.Actor));

            _context.SubStatements.Add(subStatement);

            return subStatement;
        }
    }
}
