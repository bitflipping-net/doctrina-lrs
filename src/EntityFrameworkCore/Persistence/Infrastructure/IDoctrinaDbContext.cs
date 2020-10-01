using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
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
        DbSet<VerbModel> Verbs { get; set; }
        DbSet<ActivityModel> Activities { get; set; }
        DbSet<PersonModel> People { get; set; }
        DbSet<PersonaModel> Personas { get; set; }
        DbSet<StatementModel> Statements { get; set; }
        DbSet<AgentProfileModel> AgentProfiles { get; set; }
        DbSet<ActivityProfileModel> ActivityProfiles { get; set; }
        DbSet<ActivityStateEntity> ActivityStates { get; set; }
        DbSet<Organisation> Organisations { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Store> Stores { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        ChangeTracker ChangeTracker { get; }
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        DatabaseFacade Database { get; }
    }
}
