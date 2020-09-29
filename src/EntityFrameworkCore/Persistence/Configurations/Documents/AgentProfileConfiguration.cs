using Doctrina.Domain.Models.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class AgentProfileConfiguration : IEntityTypeConfiguration<AgentProfileEntity>
    {
        public void Configure(EntityTypeBuilder<AgentProfileEntity> builder)
        {
            builder.HasBaseType<DocumentEntity>();

            builder.Property(p=> p.ProfileId)
                .HasColumnName("Key")
                .IsRequired();
                
            builder.Property(e => e.Key)
                .HasMaxLength(Constants.MAX_URL_LENGTH)
                .IsRequired();

            builder.HasOne(e => e.Persona)
                .WithMany();

            builder.HasOne(doc => doc.Activity)
                .WithMany();
        }
    }
}
