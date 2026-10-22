using API.Feature.Domain;

namespace API.Feature;

public record TradeSetIndicativePriceCommand(Side Side, int Lot, int IndicativePrice) : ICommand<Trade>;
public class TradeSetIndicativePriceCommandHandler(AppDbContext db) : ICommandHandler<TradeSetIndicativePriceCommand, Trade>
{
    public async Task<Trade> ExecuteAsync(TradeSetIndicativePriceCommand command, CancellationToken cancellation)
    {
        var trade = await db.Set<Trade>().OrderByDescending(x => x.Id).FirstAsync(x => x.Side == command.Side && x.LotAt != null && x.IndicativePriceAt == null);
        trade.SetIndicativePrice(command.IndicativePrice);
        db.Set<Trade>().Update(trade);
        await db.SaveChangesAsync(cancellation);

        return trade;
    }
}