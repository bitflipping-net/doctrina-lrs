using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StatementBaseConfiguration : IEntityTypeConfiguration<StatementBaseEntity>
    {
        public void Configure(EntityTypeBuilder<StatementBaseEntity> builder)
        {
            builder.ToTable("StatementBases");

            builder.HasBaseType<StatementObjectBaseEntity>();

            //builder.HasKey(e => e.StatementBaseId);
            builder.Property(e => e.StatementBaseId)
                .ValueGeneratedOnAdd();

            // Actor
            builder.HasOne(e => e.Actor)
                .WithMany()
                .IsRequired();

            // Verb
            builder.HasOne(e => e.Verb)
                .WithMany()
                .IsRequired();

            builder.HasOne(p => p.Object)
                .WithMany()
                .IsRequired();

            builder.HasOne(e => e.Result)
                .WithOne();

            builder.Property(e => e.Timestamp)
                .IsRequired();

            builder.HasMany(e => e.Attachments)
                .WithOne(x => x.Statement)
                .HasForeignKey(e => e.StatementId);

        }
    }
}
