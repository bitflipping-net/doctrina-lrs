using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<GroupPersona>
    {
        public void Configure(EntityTypeBuilder<GroupPersona> builder)
        {
            builder.HasBaseType<Persona>();

            builder.HasOne(e => e.Person)
                .WithMany()
                .IsRequired(false);

            builder.HasMany(x => x.Personas)
                 .WithMany();
        }
    }
}
