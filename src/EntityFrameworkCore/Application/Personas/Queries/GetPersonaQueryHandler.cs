using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Personas.Queries;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonaQueryHandler : IRequestHandler<GetPersonaQuery, PersonaModel>
    {
        private readonly IStoreDbContext _context;

        public GetPersonaQueryHandler(IStoreDbContext context)
        {
            _context = context;
        }

        public async Task<PersonaModel> Handle(GetPersonaQuery request, CancellationToken cancellationToken)
        {
            PersonaModel persona = await _context.Personas
                .FirstOrDefaultAsync(x => 
                x.StoreId == _context.StoreId
                && x.ObjectType == request.ObjectType
                && x.Key == request.Key
                && x.Value == request.Value, cancellationToken);

            return persona;
        }
    }
}
