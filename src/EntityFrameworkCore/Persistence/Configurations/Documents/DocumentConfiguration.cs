using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.HasDiscriminator<string>("DocumentType")
                .HasValue<ActivityProfileEntity>("ActivityProfile")
                .HasValue<ActivityStateEntity>("ActivityState")
                .HasValue<AgentProfileEntity>("AgentProfile");

            builder.Property(document => document.Key)
                .HasColumnName("Key")
                .IsRequired()
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(document => document.ContentType)
                .HasMaxLength(255);

            builder.Property(document => document.Content);

            builder.Property(document => document.Checksum)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(document => document.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(document => document.CreatedAt)
                .ValueGeneratedOnAdd();

            builder.HasOne(document => document.Store)
                .WithMany()
                .HasForeignKey(doc => doc.StoreId);

            builder.Property(document => document.StoreId)
                .IsRequired();
        }
    }
}
