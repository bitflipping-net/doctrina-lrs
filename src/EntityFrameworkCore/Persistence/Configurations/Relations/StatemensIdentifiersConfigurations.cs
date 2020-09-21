using Doctrina.Domain.Entities.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Relations
{
    public class StatemensIdentifiersConfigurations : IEntityTypeConfiguration<StatementIdentifiers>
    {
        public void Configure(EntityTypeBuilder<StatementIdentifiers> builder)
        {
            builder.ToTable("StatementIdentifiers");

            builder.HasOne(b => b.PersonaIdentifier)
                .WithMany()
                .HasForeignKey(b => b.IFI);

            builder.HasOne(b => b.Statement)
                .WithMany()
                .HasForeignKey(si => si.StatementId);

            builder.HasOne(b => b.Store)
                .WithMany()
                .HasForeignKey(si => si.StoreId);

            builder.HasKey(b => new { b.IFI, b.StatementId, b.StoreId });
        }
    }
}
