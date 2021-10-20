using FinancialManager.Core;
using FinancialManager.FinancialAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FinancialManager.FinancialAccounts.Data
{
    internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        private readonly Guid _tenantId;

        public ExpenseConfiguration(Guid tenantId) => _tenantId = tenantId;
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Type)
                    .IsRequired();

            builder.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(p => p.TenantId)
                    .IsRequired()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasMany(p => p.Categories)
                    .WithMany(p => p.Expenses);

            builder.HasOne(p => p.Account)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(p => p.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Amount)
                    .HasConversion(amount => amount.Value, p => new Amount(p))
                    .HasColumnType("DECIMAL(8,2)")
                    .IsRequired();

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
