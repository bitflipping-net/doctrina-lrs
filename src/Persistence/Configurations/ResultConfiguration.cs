using Doctrina.Domain.Entities;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<ResultEntity>
    {
        public void Configure(EntityTypeBuilder<ResultEntity> builder)
        {
            builder.ToTable("Results");

            builder.Property(e => e.ResultId)
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.ResultId);

            builder.OwnsOne(e => e.Score);

            builder.Property(e => e.Extensions)
                .HasConversion(new ExtensionsCollectionValueConverter())
                .HasColumnType("ntext");
        }
    }
}
