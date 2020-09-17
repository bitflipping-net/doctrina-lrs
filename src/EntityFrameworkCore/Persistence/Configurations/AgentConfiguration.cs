using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<AgentEntity>
    {
        public void Configure(EntityTypeBuilder<AgentEntity> builder)
        {
            builder.Property(x => x.AgentId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.AgentId);

            builder.ToTable("Agents")
                .HasDiscriminator<string>("ObjectType")
                .HasValue<AgentEntity>(Domain.Entities.ObjectType.Agent.ToString())
                .HasValue<GroupEntity>(Domain.Entities.ObjectType.Group.ToString());

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.HasOne(x => x.IFI)
                .WithMany();

            builder.Property(x => x.IFI)
                .HasColumnName("ifi");
        }
    }
}
