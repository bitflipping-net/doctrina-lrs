using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Persons.Queries
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonEntity>
    {
        private readonly IDoctrinaDbContext dbContext;

        public GetPersonQueryHandler(IDoctrinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<PersonEntity> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            return (
                from rel in dbContext.ObjectRelations
                join per in dbContext.Persons
                    on rel.ParentId equals per.PersonId
                where rel.ParentObjectType == EntityObjectType.Person
                && rel.ChildObjectType == request.ObjectType
                && rel.ChildId == request.AgentId
                select per
            ).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
