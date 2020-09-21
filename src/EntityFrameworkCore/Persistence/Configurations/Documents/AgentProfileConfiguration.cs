using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class AgentProfileConfiguration : IEntityTypeConfiguration<AgentProfileEntity>
    {
        public void Configure(EntityTypeBuilder<AgentProfileEntity> builder)
        {
            builder.HasBaseType<DocumentEntity>();

            builder.Property(e => e.Key);
                
            builder.Property(e => e.Key)
                .HasMaxLength(Constants.MAX_URL_LENGTH)
                .IsRequired();

            builder.HasOne(e => e.PersonaIdentifier)
                .WithMany();

            builder.HasOne(doc => doc.Activity)
                .WithMany();
        }
    }
}
