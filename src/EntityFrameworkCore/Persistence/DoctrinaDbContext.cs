using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
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

        public DbSet<PersonaModel> Personas { get; set; }
        public DbSet<PersonModel> People { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StatementModel> Statements { get; set; }
        public DbSet<VerbModel> Verbs { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<AgentProfileModel> AgentProfiles { get; set; }
        public DbSet<ActivityProfileModel> ActivityProfiles { get; set; }
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
