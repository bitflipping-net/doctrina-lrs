using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class PersonPersonaConfiguration : IEntityTypeConfiguration<PersonPersona>
    {
        public void Configure(EntityTypeBuilder<PersonPersona> builder)
        {
            builder.HasKey(x => new { x.PersonId, x.PersonaId });

            builder.HasOne(pp => pp.Person)
                .WithMany()
                .HasForeignKey(pp => pp.PersonId)
                .IsRequired(true);

            builder.HasOne(pp => pp.Persona)
                .WithMany()
                .HasForeignKey(pp=> pp.PersonaId)
                .IsRequired(true);
        }
    }
}
