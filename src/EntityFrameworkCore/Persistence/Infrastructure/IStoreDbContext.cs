using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.Domain.Models.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace Doctrina.Persistence.Infrastructure
{
    public interface IStoreDbContext : IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable, IResettableService
    {
        /// <summary>
        /// The Id of the current store
        /// </summary>
        Guid StoreId { get; }
        DbSet<VerbModel> Verbs { get; set; }
        DbSet<ActivityModel> Activities { get; set; }
        DbSet<PersonaModel> Personas { get; set; }
        DbSet<PersonModel> Persons { get; set; }
        DbSet<StatementBaseModel> Statements { get; set; }
        DbSet<DocumentModel> Documents { get; set; }

        /// <summary>
        /// Store clients authorized to pull/push statements
        /// </summary>
        DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Statement ObjectType relations (activities, verbs, substatements, statements)
        /// </summary>
        DbSet<StatementRelation> Relations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        ChangeTracker ChangeTracker { get; }
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        DatabaseFacade Database { get; }
    }
}
