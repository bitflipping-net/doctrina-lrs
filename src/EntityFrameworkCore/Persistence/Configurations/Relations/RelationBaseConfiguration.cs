using Doctrina.Domain.Models.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations.Relations
{
    public class RelationBaseConfiguration : IEntityTypeConfiguration<StatementRelation>
    {
        public void Configure(EntityTypeBuilder<StatementRelation> builder)
        {
            builder.HasKey(x => new { x.StoreId, x.ParentId, x.ChildId, x.ObjectType });

            builder.Property(p => p.StoreId)
               .IsRequired();

            builder.Property(x => x.ParentId)
                .IsRequired();

            builder.Property(p => p.ObjectType)
                .IsRequired();

            builder.Property(p => p.ChildId)
                .IsRequired();
        }
    }
}
