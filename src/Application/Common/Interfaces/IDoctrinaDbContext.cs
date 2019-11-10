using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common.Interfaces
{
    public interface IDoctrinaDbContext : IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable, IResettableService
    {
        DbSet<VerbEntity> Verbs { get; set; }
        DbSet<ActivityEntity> Activities { get; set; }
        DbSet<AgentEntity> Agents { get; set; }
        DbSet<StatementEntity> Statements { get; set; }
        DbSet<SubStatementEntity> SubStatements { get; set; }
        DbSet<AgentProfileEntity> AgentProfiles { get; set; }
        DbSet<ActivityProfileEntity> ActivityProfiles { get; set; }
        DbSet<ActivityStateEntity> ActivityStates { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        ChangeTracker ChangeTracker { get; }
    }
}
