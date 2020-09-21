using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class GroupMemberIdentifierConfiguration : IEntityTypeConfiguration<GroupMemberIdentifier>
    {
        public void Configure(EntityTypeBuilder<GroupMemberIdentifier> builder)
        {
            builder.ToTable("GroupMembers");

            builder.HasOne(g => g.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.IFI)
                .WithMany()
                .HasForeignKey(x => x.Identifier)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
