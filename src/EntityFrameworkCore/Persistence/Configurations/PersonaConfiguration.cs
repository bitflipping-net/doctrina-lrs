using Doctrina.Domain.Models;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<PersonaModel>
    {
        public void Configure(EntityTypeBuilder<PersonaModel> builder)
        {
            builder.HasKey(pi => pi.PersonaId);
            builder.Property(pi => pi.PersonaId)
                .ValueGeneratedOnAdd();

            builder.HasDiscriminator(persona => persona.ObjectType)
                .HasValue<PersonaModel>(ObjectType.Agent)
                .HasValue<PersonaGroup>(ObjectType.Group);

            builder.Property(persona => persona.ObjectType)
                .IsRequired();

            builder.Property(persona => persona.Name)
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .IsRequired(false);

            builder.Property(x => x.Key)
                .HasConversion<IFITypeValueConverter>()
                .IsRequired(false);

            builder.Property(x => x.Value)
                .IsRequired(false);

            builder.HasOne(pi => pi.Store)
                .WithMany()
                .HasForeignKey(pi => pi.StoreId)
                .IsRequired();
        }
    }
}
