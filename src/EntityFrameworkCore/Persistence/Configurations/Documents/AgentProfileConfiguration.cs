using Doctrina.Domain.Models.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class AgentProfileConfiguration : IEntityTypeConfiguration<AgentProfileModel>
    {
        public void Configure(EntityTypeBuilder<AgentProfileModel> builder)
        {
            builder.HasBaseType<DocumentModel>();

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
