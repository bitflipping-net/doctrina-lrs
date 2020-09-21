using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class StatementObjectConfiguration : IEntityTypeConfiguration<StatementObject>
    {
        public void Configure(EntityTypeBuilder<StatementObject> builder)
        {
            builder.HasKey(x => new { x.ObjectType, x.ObjectId, x.StoreId, x.StatementId });

            builder.HasDiscriminator<string>("ObjectType")
                .HasValue<Agent>("Agent")
                .HasValue<Domain.Entities.GroupPersona>("Group");
        }
    }
}
