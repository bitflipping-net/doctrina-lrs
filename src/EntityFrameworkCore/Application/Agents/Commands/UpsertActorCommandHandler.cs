using AutoMapper;
using Doctrina.Application.Agents.Notifications;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Infrastructure.ExperienceApi;
using Doctrina.Application.Personas.Commands;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommandHandler : IRequestHandler<UpsertActorCommand, PersonaModel>
    {
        private readonly IStoreDbContext _storeDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpsertActorCommandHandler(IStoreDbContext storeDbContext, IMediator mediator, IMapper mapper)
        {
            _storeDbContext = storeDbContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PersonaModel> Handle(UpsertActorCommand request, CancellationToken cancellationToken)
        {
            Agent actor = request.Actor;
            Guid storeId = _storeDbContext.StoreId;
            if (actor is Agent agent)
            {
                var cmd = UpsertPersonaCommand.CreateAgent(
                    storeId,
                    agent.GetIdentifierKey(),
                    agent.GetIdentifierValue(),
                    agent.Name
                );

                return await _mediator.Send(cmd, cancellationToken);
            }
            else if (actor is Group group)
            {
                ICollection<PersonaModel> members = new HashSet<PersonaModel>();

                foreach (Agent member in group.Member)
                {
                    members.Add(await Handle(UpsertActorCommand.Create(member), cancellationToken));
                }

                if (group.IsAnonymous())
                {
                    return await _mediator.Send(UpsertPersonaCommand.CreateAnonymousGroup(
                        storeId,
                        members,
                        group.Name
                    ));
                }
                else
                {
                    return await _mediator.Send(UpsertPersonaCommand.CreateIdentifiedGroup(
                        storeId,
                        group.GetIdentifierKey(),
                        group.GetIdentifierValue(),
                        members,
                        group.Name
                    ));
                }
            }

            throw new NotImplementedException();
        }
    }
} 
