using Doctrina.Domain.Models.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityStateConfiguration : IEntityTypeConfiguration<ActivityStateEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityStateEntity> builder)
        {
            builder.HasBaseType<DocumentModel>();

            builder.Property(p => p.Key);

            builder.HasOne(e => e.Activity)
                .WithMany();

            builder.Property(x => x.RegistrationId);

            builder.HasOne(e => e.Persona)
                .WithMany();
        }
    }
}
