using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<AgentEntity>
    {
        public void Configure(EntityTypeBuilder<AgentEntity> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);

            builder.ToTable("Agents")
                .HasDiscriminator<string>("ObjectType")
                .HasValue<AgentEntity>(ObjectType.Agent.ToString())
                .HasValue<GroupPersona>(ObjectType.Group.ToString());

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.HasOne(x => x.Persona)
                .WithMany()
                .IsRequired();
        }
    }
}
