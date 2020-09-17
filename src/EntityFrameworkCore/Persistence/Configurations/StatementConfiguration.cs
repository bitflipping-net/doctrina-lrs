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
                .HasForeignKey(st => st.ActorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            // Verb
            builder.HasOne(e => e.Verb)
                .IsRequired();

            builder.OwnsOne(p => p.Object);

            builder.HasOne(e => e.Result)
                .WithMany();

            builder.HasOne(e => e.Context)
               .WithMany();

            builder.Property(e => e.Timestamp)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Attachments)
                .WithOne();

            builder.Property(e => e.CreatedAt)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(e => e.Version)
                .HasMaxLength(7);

            builder.HasOne(e => e.Authority)
                .WithMany()
                .HasForeignKey(e => e.AuthorityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.VoidingStatement)
                .WithMany()
                .HasForeignKey(c => c.VoidingStatementId)
                .IsRequired(false);
        }
    }
}
