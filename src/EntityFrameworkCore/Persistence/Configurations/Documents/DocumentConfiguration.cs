using Doctrina.Domain.Models.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentModel>
    {
        public void Configure(EntityTypeBuilder<DocumentModel> builder)
        {
            builder.HasKey(p => new { p.StoreId, p.Key });

            builder.HasDiscriminator<string>("DocumentType")
                .HasValue<ActivityProfileModel>("ActivityProfile")
                .HasValue<ActivityStateEntity>("ActivityState")
                .HasValue<AgentProfileModel>("AgentProfile");

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
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.Property(document => document.CreatedAt)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasOne(document => document.Store)
                .WithMany()
                .HasForeignKey(doc => doc.StoreId);

            builder.Property(document => document.StoreId)
                .IsRequired();
        }
    }
}
