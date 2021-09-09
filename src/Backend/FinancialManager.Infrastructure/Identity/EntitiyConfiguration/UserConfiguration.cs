using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManager.Infrastructure.Identity
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(p => p.Created)
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(p => p.LastUpdated)
                    .HasDefaultValue(null)
                    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Ignore(p => p.IsValid);
            builder.Ignore(p => p.IsInvalid);
            builder.Ignore(p => p.ValidationMessages);

            builder.HasQueryFilter(p => p.IsActived);
        }
    }
}
