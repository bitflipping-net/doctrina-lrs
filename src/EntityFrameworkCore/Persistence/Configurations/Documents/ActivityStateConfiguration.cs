using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityStateConfiguration : IEntityTypeConfiguration<ActivityStateEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityStateEntity> builder)
        {
            builder.HasBaseType<DocumentEntity>();

            builder.Property(e => e.Key);

            builder.HasOne(e => e.Activity)
                .WithMany();

            builder.Property(x => x.RegistrationId);

            builder.HasOne(e => e.PersonaIdentifier)
                .WithMany();
        }
    }
}
