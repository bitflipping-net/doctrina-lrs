using Doctrina.Domain.Entities.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Relations
{
    public class StatementVerbsConfiguration : IEntityTypeConfiguration<StatementVerbs>
    {
        public void Configure(EntityTypeBuilder<StatementVerbs> builder)
        {
            builder.ToTable("StatementIdentifiers");

            builder.HasKey(b => new { b.StatementId, b.VerbId, b.StoreId });

            builder.HasOne(x => x.Statement)
                .WithMany()
                .HasForeignKey(x => x.StatementId);

            builder.HasOne(x => x.Verb)
                .WithMany()
                .HasForeignKey();

            builder.HasOne(x => x.Store)
                .WithMany()
                .HasForeignKey(x => x.StoreId);
        }
    }
}
