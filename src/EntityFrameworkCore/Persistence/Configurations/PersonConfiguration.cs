using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(p => p.PersonId);

            builder.Property(p => p.PersonId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired();
        }
    }
}
