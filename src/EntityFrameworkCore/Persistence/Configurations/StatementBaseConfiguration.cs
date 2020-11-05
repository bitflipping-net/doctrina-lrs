using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Persistence.Configurations
{
    public class StatementBaseConfiguration : IEntityTypeConfiguration<StatementBaseModel>
    {
        public void Configure(EntityTypeBuilder<StatementBaseModel> builder)
        {
            builder.ToTable("Statements");

            builder.Property("StatementTypeName")
                .HasColumnName("StatementTypeName")
                .HasConversion(new StringToEnumConverter<StatementType>())
                .IsRequired();

            builder.HasKey(p => p.StatementId);
            builder.Property(p => p.StatementId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StoreId)
                .IsRequired();

            builder.HasOne(e => e.Persona)
                .WithMany()
                .HasForeignKey(x=> x.PersonaId)
                .IsRequired();

            // Verb
            builder.HasOne(e => e.Verb)
                .WithMany()
                .IsRequired();

            builder.Property(x => x.ObjectId)
                .IsRequired();

            builder.Property(x => x.ObjectType)
                .HasConversion<string>(new EnumToStringConverter<ObjectType>())
                .IsRequired();

            builder.HasOne(e => e.Result)
                .WithMany()
                .IsRequired(false);

            builder.HasOne(e => e.Context)
               .WithMany()
               .IsRequired(false);

            builder.Property(e => e.Timestamp)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Attachments)
                .WithOne()
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
               .IsRequired()
               .ValueGeneratedOnAdd();
        }
    }
}
