using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Persistence.Configurations
{
    public class SubStatementConfiguration : IEntityTypeConfiguration<SubStatementEntity>
    {
        public void Configure(EntityTypeBuilder<SubStatementEntity> builder)
        {
            builder.ToTable("SubStatements");

            builder.HasKey(x=> x.SubStatementId);

            builder.Property(x=> x.SubStatementId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Actor
            builder.HasOne(e => e.Actor)
                .WithMany()
                .IsRequired();

            // Verb
            builder.HasOne(e => e.Verb)
                .WithMany()
                .IsRequired();

            builder.Property(x => x.ObjectType)
                .HasConversion(new EnumToStringConverter<EntityObjectType>())
                .IsRequired();

            builder.Property(x => x.ObjectId)
                .IsRequired();

            builder.Ignore(x => x.Object);

            builder.HasOne(e => e.Result)
                .WithMany();

            builder.HasOne(e => e.Context)
                .WithMany();

            builder.Property(e => e.Timestamp)
                .IsRequired();

            builder.HasMany(x => x.Attachments)
                .WithOne();
        }
    }
}
