﻿using FinancialManager.FinancialAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FinancialManager.FinancialAccounts.Data
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly Guid _tenantId;

        public CategoryConfiguration(Guid tenantId) => _tenantId = tenantId;
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(p => p.TenantId)
                    .IsRequired()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasMany(p => p.Expenses)
                    .WithMany(p => p.Categories);

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
