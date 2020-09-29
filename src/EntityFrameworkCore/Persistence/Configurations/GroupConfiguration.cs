using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<PersonaGroup>
    {
        public void Configure(EntityTypeBuilder<PersonaGroup> builder)
        {
            builder.HasBaseType<Persona>();

            builder.HasMany(x => x.Members)
                 .WithOne();
        }
    }
}
