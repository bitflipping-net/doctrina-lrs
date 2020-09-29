using Doctrina.Application.Personas.Commands;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Personas.Command
{
    public class UpsertPersonaCommandHandler : IRequestHandler<UpsertPersonaCommand, Persona>
    {
        private readonly IDoctrinaDbContext _dbContext;

        public UpsertPersonaCommandHandler(IDoctrinaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Persona> Handle(UpsertPersonaCommand request, CancellationToken cancellationToken)
        {
            Persona persona = null;
            if (request.Key != null)
            {
                persona = _dbContext.Personas.SingleOrDefault(x =>
                    x.StoreId == request.StoreId
                    && x.ObjectType == request.Type
                    && x.Key == request.Key
                    && x.Value == request.Value
                );
            }

            if (persona == null)
            {
                // Create new persona
                persona = new Persona()
                {
                    ObjectType = request.Type,
                    Key = request.Key,
                    Name = request.Name,
                    Value = request.Value,
                    StoreId = request.StoreId
                };

                _dbContext.Personas.Add(persona);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            return persona;
        }
    }
}
