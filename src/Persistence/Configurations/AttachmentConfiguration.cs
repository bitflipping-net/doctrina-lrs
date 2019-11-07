using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentEntity>
    {
        public void Configure(EntityTypeBuilder<AttachmentEntity> builder)
        {
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
                .HasConversion(new LanguageMapCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(e => e.Description)
                .HasConversion(new LanguageMapCollectionValueConverter())
                .HasColumnType("ntext");

            builder.Property(e => e.Length)
                .IsRequired();
        }
    }
}
