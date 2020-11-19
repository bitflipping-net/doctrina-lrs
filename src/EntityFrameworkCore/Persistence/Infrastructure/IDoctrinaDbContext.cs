using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Configurations.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Persistence.Infrastructure
{
    public interface IDoctrinaDbContext : IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable, IResettableService
    {
        DbSet<VerbEntity> Verbs { get; }
        DbSet<ActivityEntity> Activities { get; }
        DbSet<AgentEntity> Agents { get; }
        DbSet<StatementEntity> Statements { get; }
        DbSet<SubStatementEntity> SubStatements { get; }
        DbSet<DocumentEntity> Documents { get; }
        DbSet<Client> Clients { get; }
        DbSet<ObjectRelation> ObjectRelations { get; set; }
        DbSet<PersonEntity> Persons { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        ChangeTracker ChangeTracker { get; }

        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
