﻿using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            builder.HasBaseType<AgentEntity>();

            builder.HasMany(e => e.Members)
                .WithOne()
                .HasForeignKey(x => x.GroupId);
        }
    }
}
