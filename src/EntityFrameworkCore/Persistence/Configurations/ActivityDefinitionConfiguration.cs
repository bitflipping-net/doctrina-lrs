using Doctrina.Domain.Models;
using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Domain.Models.ValueObjects;
using Doctrina.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class ActivityDefinitionConfiguration : IEntityTypeConfiguration<ActivityDefinitionEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityDefinitionEntity> builder)
        {
            builder.ToTable("ActivityDefinitions");

            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .ValueGeneratedOnAdd()
            //    .IsRequired();

            //builder.HasOne(x => x.Statement)
            //    .WithMany()
            //    .HasForeignKey(x => x.StatementId);

            builder.Property(e => e.Type)
                .IsRequired(false);

            builder.Property(e => e.MoreInfo)
                .IsRequired(false);

            builder.Property(e => e.InteractionActivity)
                .HasConversion(new JsonValueConverter<InteractionActivityBase>())
                .IsRequired(false);

            builder.Property(p => p.Names)
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.Property(p => p.Descriptions)
                .HasConversion(new JsonValueConverter<LanguageMapCollection>())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<LanguageMapCollection>(false));

            builder.Property(p => p.Extensions)
                .HasConversion(new ExtensionsCollectionValueConverter())
                .HasColumnType("ntext")
                .Metadata
                .SetValueComparer(new ValueComparer<ExtensionsCollection>(false));
        }
    }
}
