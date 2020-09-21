using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StatementConfiguration : IEntityTypeConfiguration<StatementEntity>
    {
        public void Configure(EntityTypeBuilder<StatementEntity> builder)
        {
            builder.ToTable("Statements");

            builder.Property(p => p.StatementId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey(p => p.StatementId);

            // Actor
            builder.HasOne(e => e.Actor)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            // Verb
            builder.HasOne(e => e.Verb)
                .WithMany()
                .IsRequired();

            builder.HasOne(p => p.Object)
                .WithOne();

            builder.HasOne(e => e.Result)
                .WithMany()
                .IsRequired(false);

            builder.HasOne(e => e.Context)
               .WithMany()
               .IsRequired(false);

            builder.Property(e => e.Timestamp)
                .IsRequired(true)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Attachments)
                .WithOne()
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(e => e.Version)
                .HasMaxLength(7)
                .IsRequired(true);

            builder.Property(e => e.Authority)
                .IsRequired();

            builder.HasOne(e => e.VoidingStatement)
                .WithMany()
                .HasForeignKey(c => c.VoidingStatementId)
                .IsRequired(false);
        }
    }
}
