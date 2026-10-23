using Common.Entity;

namespace App.Feature.Domain;

public partial class Trade: BaseEntity
{
    private Trade() { }
    public long UserId { get; private set; }
    public Side Side { get; private set; }
    public int Geram { get; private set; }
    public decimal Price { get; private set; }
}

public enum Side { Buy, Sell }

public class TradeConfig : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
    }
}