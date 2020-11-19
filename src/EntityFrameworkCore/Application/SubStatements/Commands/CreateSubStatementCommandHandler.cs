using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.SubStatements
{
    public class CreateSubStatementCommandHandler : BaseStatementCommandHandler, IRequestHandler<CreateSubStatementCommand, SubStatementEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSubStatementCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<SubStatementEntity> Handle(CreateSubStatementCommand request, CancellationToken cancellationToken)
        {
            var subStatement = new SubStatementEntity();

            await HandleStatementBase(request.SubStatement, (IStatementBaseEntity)subStatement, cancellationToken);

            _context.SubStatements.Add(subStatement);

            return subStatement;
        }
    }
}
