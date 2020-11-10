using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Persistence
{
    public partial class DoctrinaDbContext : DbContext, IDoctrinaDbContext
    {
        public DoctrinaDbContext(DbContextOptions<DoctrinaDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<VerbEntity> Verbs { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<ActivityEntity> Activities { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<AgentEntity> Agents { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<StatementEntity> Statements { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<SubStatementEntity> SubStatements { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<AgentProfileEntity> AgentProfiles { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<ActivityProfileEntity> ActivityProfiles { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<ActivityStateEntity> ActivityStates { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctrinaDbContext).Assembly);
        }
    }

}
