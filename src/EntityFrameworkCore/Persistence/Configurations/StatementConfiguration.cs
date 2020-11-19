using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
                .WithMany()
                .HasForeignKey(st => st.VerbId)
                .IsRequired();

            builder.Property(x => x.ObjectType)
                .HasConversion(new EnumToStringConverter<EntityObjectType>())
                .IsRequired();

            builder.Property(x => x.ObjectId)
                .IsRequired();

            builder.Ignore(x=> x.Object);

            builder.HasOne(e => e.Result)
                .WithMany()
                .HasForeignKey(x=> x.ResultId);

            builder.HasOne(e => e.Context)
               .WithMany()
               .HasForeignKey(x=> x.ContextId);

            builder.Property(e => e.Timestamp)
                .IsRequired();

            builder.HasMany(x => x.Attachments)
                .WithOne();

            builder.Property(e => e.Stored)
               .IsRequired();

            builder.Property(e => e.Version)
                .HasMaxLength(7);

            builder.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.VoidingStatement)
                .WithMany()
                .HasForeignKey(c => c.VoidingStatementId)
                .IsRequired(false);
        }
    }
}
