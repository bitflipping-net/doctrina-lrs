using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class PersonaGroupConfiguration : IEntityTypeConfiguration<PersonaGroup>
    {
        public void Configure(EntityTypeBuilder<PersonaGroup> builder)
        {
            builder.HasBaseType<PersonaModel>();

            builder.HasMany(x => x.Members)
                 .WithOne();
        }
    }
}
