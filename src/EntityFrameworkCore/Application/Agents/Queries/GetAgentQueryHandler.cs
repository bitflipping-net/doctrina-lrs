using AutoMapper;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentQueryHandler : IRequestHandler<GetAgentQuery, Persona>
    {
        private readonly IStoreDbContext _context;
        private IMapper _mapper;

        public GetAgentQueryHandler(IStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Persona> Handle(GetAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = _mapper.Map<Persona>(request.Agent);

            Persona persona = await _context.Personas
                .FirstOrDefaultAsync(x => 
                x.StoreId == _context.StoreId
                && x.ObjectType == agent.ObjectType
                && x.Key == agent.Key
                && x.Value == agent.Value, cancellationToken);

            return persona;
        }
    }
}
