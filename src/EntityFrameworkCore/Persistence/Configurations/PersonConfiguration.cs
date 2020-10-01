using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonModel>
    {
        public void Configure(EntityTypeBuilder<PersonModel> builder)
        {
            builder.HasKey(x => x.PersonId);

            builder.HasIndex(x => new { x.PersonId, x.StoreId })
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(Constants.MAX_NAME_LENGTH);

            builder.HasMany(person => person.Personas)
                .WithOne();
        }
    }
}
