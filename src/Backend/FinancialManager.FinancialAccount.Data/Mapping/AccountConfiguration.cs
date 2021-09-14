using FinancialManager.FinancialAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FinancialManager.FinancialAccounts.Data
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        private readonly Guid _ownerId;
        public AccountConfiguration(Guid ownerId) => _ownerId = ownerId;

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("FinancialAccounts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AccountName)
                    .HasMaxLength(30)
                    .IsRequired();

            builder.Property(p => p.AccountType)
                    .HasColumnType("tinyint")
                    .IsRequired();

            builder.Property(p => p.OwnerId)
                    .IsRequired()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(p => p.Created)
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(p => p.LastUpdated)
                    .HasDefaultValue(null)
                    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(p => p.IsDeleted)
                    .HasDefaultValue(false);

            builder.Ignore(p => p.Notifications);

            builder.HasQueryFilter(p => !p.IsDeleted && p.OwnerId == _ownerId);
        }
    }
}
