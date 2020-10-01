using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.Persons.Queries
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonModel>
    {
        private readonly IStoreDbContext _context;

        public GetPersonQueryHandler(IStoreDbContext context)
        {
            _context = context;
        }

        public async Task<PersonModel> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {

            PersonModel model = await _context.Persons
                .Where(person => person.StoreId == _context.StoreId)
                .Where(person=> person.Personas.Any(rel => rel.PersonaId == request.Persona.PersonaId))
                .SingleOrDefaultAsync(cancellationToken);

            if (model == null)
            {
                model = new PersonModel();
                model.Personas.Add(new PersonPersona()
                {
                    Persona = request.Persona
                });
            }

            return model;
        }
    }
}
