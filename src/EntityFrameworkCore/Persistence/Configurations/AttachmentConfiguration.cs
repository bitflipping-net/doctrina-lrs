using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentEntity>
    {
        public void Configure(EntityTypeBuilder<AttachmentEntity> builder)
        {
            builder.ToTable("Attachments");

            builder.Property(e => e.UsageType)
                .IsRequired()
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.SHA2)
                .IsRequired();

            builder.Property(e => e.Display)
                .IsRequired()
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.Property(e => e.Description)
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.Property(e => e.Length)
                .IsRequired();
        }
    }
}
