using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            //builder.ToTable("Groups");

            builder.HasBaseType<AgentEntity>();

            builder.HasMany(e => e.Members)
                .WithOne();
        }
    }
}
