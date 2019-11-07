using Doctrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Doctrina.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property<Guid>("AccountId")
                .ValueGeneratedOnAdd();
            builder.HasKey("AccountId");

            builder.Property(e => e.HomePage)
                .HasMaxLength(Constants.MAX_URL_LENGTH);

            builder.Property(e => e.Name)
               .HasMaxLength(40);

            builder
                .HasIndex(account => new { account.HomePage, account.Name })
                .HasFilter("[HomePage] IS NOT NULL AND [Name] IS NOT NULL")
                .IsUnique();

            builder.ToTable("AgentAccounts");
        }
    }
}
