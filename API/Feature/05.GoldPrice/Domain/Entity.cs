using Common.Entity;

namespace API.Feature.Domain;

public partial class GoldPrice : BaseEntity
{
    private GoldPrice() { }
    public string Symbol { get; private set; } = null!;
    public decimal Sell { get; private set; }
    public decimal Buy { get; private set; }
    public DateTime LastUpdate { get; private set; }
}

public class GoldPriceConfig : IEntityTypeConfiguration<GoldPrice>
{
    public void Configure(EntityTypeBuilder<GoldPrice> builder)
    {
        builder.Property(x => x.Symbol).HasMaxLength(20);
        builder.Property(x => x.Sell).HasPrecision(18, 2);
        builder.Property(x => x.Buy).HasPrecision(18, 2);
        builder.HasIndex(x => new { x.Symbol, x.LastUpdate }).IsUnique();
    }
}
