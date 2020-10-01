using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StatementConfiguration : IEntityTypeConfiguration<StatementModel>
    {
        public void Configure(EntityTypeBuilder<StatementModel> builder)
        {
            builder.ToTable("Statements");

            builder.HasBaseType<StatementBaseModel>();

            //builder.Property(e => e.Version)
            //    .HasMaxLength(7)
            //    .IsRequired();

            builder.HasOne(stmt => stmt.Client)
                .WithMany()
                .HasForeignKey(stmt => stmt.ClientId)
                .IsRequired();

            builder.HasOne(e => e.VoidingStatement)
                .WithMany()
                .HasForeignKey(c => c.VoidingStatementId)
                .IsRequired(false);

            builder.Property(stmt => stmt.VoidedAt)
                .IsRequired(false);
        }
    }
}
