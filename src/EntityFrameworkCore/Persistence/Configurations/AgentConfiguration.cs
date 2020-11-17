using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Persistence.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<AgentEntity>
    {
        public void Configure(EntityTypeBuilder<AgentEntity> builder)
        {
            builder.ToTable("Agents");

            builder.Property(x => x.AgentId)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.AgentId);

            builder.HasDiscriminator(x => x.ObjectType)
                .HasValue<AgentEntity>(EntityObjectType.Agent)
                .HasValue<GroupEntity>(EntityObjectType.Group);

            builder.Property(x => x.ObjectType)
                .HasConversion(new EnumToStringConverter<EntityObjectType>())
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.IFI_Key)
                .HasMaxLength("mbox_sha1sum".Length);

            builder.Property(e => e.IFI_Value)
                .HasMaxLength(40)
                .HasColumnName("Mbox_SHA1SUM");

            //builder
            //    .HasIndex(x => new { x.ObjectType, x.Mbox })
            //    .HasFilter("[Mbox] IS NOT NULL")
            //    .IsUnique();

            //builder
            //    .HasIndex(x => new { x.ObjectType, x.Mbox_SHA1SUM })
            //    .HasFilter("[Mbox_SHA1SUM] IS NOT NULL")Accou
            //    .IsUnique();

            //builder
            //    .HasIndex(x => new { x.ObjectType, x.OpenId })
            //    .HasFilter("[OpenId] IS NOT NULL")
            //    .IsUnique();

            //// TODO: We need to make sure accounts are Agents or Groups unique when identified with an account
            //builder
            //    .HasIndex("ObjectType", "AccountId")
            //    .HasFilter("[AccountId] IS NOT NULL")
            //    .IsUnique();
        }
    }
}
