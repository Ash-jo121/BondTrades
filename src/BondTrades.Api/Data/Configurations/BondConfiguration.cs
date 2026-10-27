using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BondConfiguration : IEntityTypeConfiguration<Bond>
{
    public void Configure(EntityTypeBuilder<Bond> builder)
    {
        // configuration
        builder.HasData(new Bond
        {
            Id = 1,
            Isin = "US1234567890",
            Issuer = "Example Corp",
            Currency = "USD",
            CouponRate = 4.25m,
            MaturityDate = new DateOnly(2030, 6, 15)
        },
        new Bond
        {
            Id = 2,
            Isin = "GB1234567890",
            Issuer = "Example Bank",
            Currency = "GBP",
            CouponRate = 5.1m,
            MaturityDate = new DateOnly(2032, 3, 20)
        }, new Bond
        {
            Id = 3,
            Isin = "US1324567890",
            Issuer = "Example Energy Corp",
            Currency = "USD",
            CouponRate = 6.25m,
            MaturityDate = new DateOnly(2030, 6, 20)
        });

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Isin)
            .IsRequired()
            .HasMaxLength(12);

        builder.HasIndex(b => b.Isin)
            .IsUnique();

        builder.Property(b => b.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(b => b.Issuer)
            .IsRequired();

        builder.Property(b => b.CouponRate)
            .HasPrecision(18, 6);
    }
}