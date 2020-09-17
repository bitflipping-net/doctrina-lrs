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

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StatementEntity> Statements { get; set; }
        public DbSet<VerbEntity> Verbs { get; set; }
        public DbSet<ActivityEntity> Activities { get; set; }
        public DbSet<AgentProfileEntity> AgentProfiles { get; set; }
        public DbSet<ActivityProfileEntity> ActivityProfiles { get; set; }
        public DbSet<ActivityStateEntity> ActivityStates { get; set; }

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
