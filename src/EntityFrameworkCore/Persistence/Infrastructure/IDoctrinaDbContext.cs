using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
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
        DbSet<VerbEntity> Verbs { get; set; }
        DbSet<ActivityEntity> Activities { get; set; }
        DbSet<Persona> Personas { get; set; }
        DbSet<StatementEntity> Statements { get; set; }
        DbSet<AgentProfileEntity> AgentProfiles { get; set; }
        DbSet<ActivityProfileEntity> ActivityProfiles { get; set; }
        DbSet<ActivityStateEntity> ActivityStates { get; set; }
        DbSet<Organisation> Organisations { get; set; }
        DbSet<Client> Clients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        ChangeTracker ChangeTracker { get; }
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        DatabaseFacade Database { get; }
    }
}
