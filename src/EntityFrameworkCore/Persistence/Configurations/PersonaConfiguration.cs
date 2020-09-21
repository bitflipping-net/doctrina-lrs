using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.HasKey(pi => pi.Id);
            builder.Property(pi => pi.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Type)
                .HasConversion<IFITypeValueConverter>()
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.HasOne(pi => pi.Store)
                .WithMany()
                .HasForeignKey(pi => pi.StoreId)
                .IsRequired();
        }
    }
}
