using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
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
                .IsRequired()
                .HasMaxLength(Constants.OBJECT_TYPE_LENGTH);

            builder.HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(x => x.PersonId);

            builder.Property(e => e.IFI_Key)
            .HasConversion(new IfiToStringConverter())
                .HasMaxLength(Constants.IFI_KEY_LENGTH);

            builder.Property(e => e.IFI_Value)
                .HasMaxLength(Constants.IFI_VALUE_LENGTH);

            builder.HasIndex(x => new { x.ObjectType, x.IFI_Key, x.IFI_Value })
            .IsUnique()
            .HasFilter("[IFI_Value] IS NOT NULL AND [IFI_Key] IS NOT NULL");
        }
    }
}
