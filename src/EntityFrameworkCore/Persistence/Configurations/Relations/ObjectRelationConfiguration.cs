using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Persistence.Configurations.Relations
{
    public class ObjectRelationConfiguration : IEntityTypeConfiguration<ObjectRelation>
    {
        public void Configure(EntityTypeBuilder<ObjectRelation> builder)
        {
            builder.ToTable("ObjectRelations");

            builder.HasKey(x => new { x.ChildObjectType, x.ParentId, x.ChildId });

            builder.Property(x => x.ChildObjectType)
                .HasConversion(new EnumToStringConverter<EntityObjectType>())
                .IsRequired();

            builder.Property(p => p.ParentId);

            builder.Property(p => p.ChildId);
        }
    }
}
