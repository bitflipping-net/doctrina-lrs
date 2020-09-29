using Doctrina.Domain.Models;
using Doctrina.Domain.Models.ValueObjects;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<ResultModel>
    {
        public void Configure(EntityTypeBuilder<ResultModel> builder)
        {
            builder.ToTable("Results");

            builder.Property(e => e.ResultId)
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.ResultId);

            builder.Property(e => e.Score)
                .HasConversion(new JsonValueConverter<Score>())
                .IsRequired(false);

            builder.Property(e => e.Extensions)
                .HasConversion(new ExtensionsCollectionValueConverter())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<ExtensionsCollection>(false));
        }
    }
}
