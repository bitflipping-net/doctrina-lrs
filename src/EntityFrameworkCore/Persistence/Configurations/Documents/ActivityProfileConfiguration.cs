using Doctrina.Domain.Models.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityProfileConfiguration : IEntityTypeConfiguration<ActivityProfileModel>
    {
        public void Configure(EntityTypeBuilder<ActivityProfileModel> builder)
        {
            builder.HasBaseType<DocumentModel>();

            builder.Property(e => e.Key)
                .IsRequired();

            builder.HasOne(e => e.Activity)
                .WithMany()
                .IsRequired();
        }
    }
}
