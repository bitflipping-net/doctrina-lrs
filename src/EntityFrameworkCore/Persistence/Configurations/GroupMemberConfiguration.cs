using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMemberEntity>
    {
        public void Configure(EntityTypeBuilder<GroupMemberEntity> builder)
        {
            builder.ToTable("GroupMembers");

            builder.HasKey(g => new { g.GroupId, g.AgentId });

            builder.HasOne(g => g.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Agent)
                .WithMany()
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
