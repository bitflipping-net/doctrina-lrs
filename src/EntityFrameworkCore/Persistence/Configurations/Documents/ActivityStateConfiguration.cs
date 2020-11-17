using Doctrina.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Documents
{
    public class ActivityStateConfiguration : IEntityTypeConfiguration<ActivityStateEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityStateEntity> builder)
        {
            builder.HasBaseType<DocumentEntity>();

            builder.HasOne(e => e.Activity)
                .WithMany()
                .HasForeignKey(x => x.ActivityId);

            builder.HasOne(e => e.Agent)
                .WithMany()
                .HasForeignKey(x=> x.AgentId);
        }
    }
}
