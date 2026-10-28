using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TradeConfiguration: IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id);

        builder.HasIndex(b => b.Book);

        builder.Property(b => b.Side);

        builder.Property(b => b.Quantity);

        builder.Property(b => b.Price);
        builder.Property(b => b.TradeDate);
        builder.Property(b => b.Status);
        builder.Property(b => b.RowVersion);
        builder.Property(b => b.IdempotencyKey);
        builder.Property(b => b.CreatedAt);
    }
}