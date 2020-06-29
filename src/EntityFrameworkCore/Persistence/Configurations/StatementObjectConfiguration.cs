using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class StatementObjectConfiguration : IEntityTypeConfiguration<StatementObjectEntity>
    {
        public void Configure(EntityTypeBuilder<StatementObjectEntity> builder)
        {
            builder.Property(x => x.ObjectType)
                .IsRequired();

            builder.HasOne(x => x.Activity).WithMany();
            builder.HasOne(x => x.Agent).WithMany(); // Or group
            builder.HasOne(x => x.SubStatement).WithMany();
            builder.HasOne(x => x.StatementRef).WithMany();
        }
    }
}
