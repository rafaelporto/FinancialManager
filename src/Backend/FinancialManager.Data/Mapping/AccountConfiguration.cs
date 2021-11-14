using FinancialManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FinancialManager.Data
{
    internal class AccountConfiguration : IEntityTypeConfiguration<FinancialAccount>
    {
        private readonly Guid _tenantId;
        public AccountConfiguration(Guid tenantId) => _tenantId = tenantId;

        public void Configure(EntityTypeBuilder<FinancialAccount> builder)
        {
            builder.ToTable("FinancialAccounts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AccountName)
                    .HasMaxLength(30)
                    .IsRequired();

            builder.Property(p => p.AccountType)
                    .HasColumnType("tinyint")
                    .IsRequired();

            builder.Property(p => p.TenantId)
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

            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasQueryFilter(p => p.TenantId == _tenantId);
        }
    }
}
