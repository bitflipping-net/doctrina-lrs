using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.ToTable("Documents");

            builder.HasDiscriminator<string>("DocumentType")
                .HasValue<ActivityProfileEntity>("ActivityProfile")
                .HasValue<ActivityStateEntity>("ActivityState")
                .HasValue<AgentProfileEntity>("AgentProfile");

            builder.HasKey(x => x.DocumentId);

            builder.Property(x => x.Key)
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(e => e.ContentType)
                    .HasMaxLength(255);

            builder.Property(e => e.Content);

            builder.Property(e => e.Checksum)
                .IsRequired()
                .HasColumnType("varbinary(64)");

            builder.Property(e => e.UpdatedAt)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();
        }
    }
}
