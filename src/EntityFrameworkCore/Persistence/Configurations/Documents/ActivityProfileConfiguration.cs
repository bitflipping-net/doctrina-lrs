using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityProfileConfiguration : IEntityTypeConfiguration<ActivityProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityProfileEntity> builder)
        {
            builder.HasBaseType<DocumentEntity>();

            builder.Property(e => e.Key)
                .IsRequired();

            builder.HasOne(e => e.Activity)
                .WithMany()
                .IsRequired();
        }
    }
}
