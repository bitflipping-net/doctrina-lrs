using Doctrina.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class SubStatementConfiguration : IEntityTypeConfiguration<SubStatementEntity>
    {
        public void Configure(EntityTypeBuilder<SubStatementEntity> builder)
        {
            builder.ToTable("Statements");

            builder.HasBaseType<StatementBaseModel>();
        }
    }
}
