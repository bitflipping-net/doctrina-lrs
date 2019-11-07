using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<AgentEntity>
    {
        public void Configure(EntityTypeBuilder<AgentEntity> builder)
        {
            builder.Property(x=> x.AgentId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.AgentId);

            builder.HasDiscriminator(x => x.ObjectType)
                .HasValue<AgentEntity>(EntityObjectType.Agent)
                .HasValue<GroupEntity>(EntityObjectType.Group);

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.Mbox)
                .HasMaxLength(128)
                .HasColumnName("Mbox");

            builder.Property(e => e.Mbox_SHA1SUM)
                .HasMaxLength(40)
                .HasColumnName("Mbox_SHA1SUM");

            builder.Property(e => e.OpenId)
               .HasColumnName("OpenId");

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey("AccountId");

            builder
                .HasIndex(x => new { x.ObjectType, x.Mbox })
                .HasFilter("[Mbox] IS NOT NULL")
                .IsUnique();

            builder
                .HasIndex(x => new { x.ObjectType, x.Mbox_SHA1SUM })
                .HasFilter("[Mbox_SHA1SUM] IS NOT NULL")
                .IsUnique();

            builder
                .HasIndex(x => new { x.ObjectType, x.OpenId })
                .HasFilter("[OpenId] IS NOT NULL")
                .IsUnique();

            // TODO: We need to make sure accounts are Agents or Groups unique when identified with an account
            builder
                .HasIndex("ObjectType", "AccountId")
                .HasFilter("[AccountId] IS NOT NULL")
                .IsUnique();
        }
    }
}
