using API.Feature.Domain;

namespace API.Feature;

public record TradeSetIndicativePriceCommand(Side Side, int Lot, int IndicativePrice) : ICommand<Trade>;

public class TradeSetIndicativePriceCommandHandler(IServiceScopeFactory factory) : ICommandHandler<TradeSetIndicativePriceCommand, Trade>
{
    public async Task<Trade> ExecuteAsync(TradeSetIndicativePriceCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var trade = await db.Set<Trade>().OrderByDescending(x => x.Id)
            .FirstAsync(x => x.Side == command.Side && x.GeramAt != null && x.IndicativePriceAt == null, cancellation);
        trade.SetIndicativePrice(command.IndicativePrice);
        db.Set<Trade>().Update(trade);
        await db.SaveChangesAsync(cancellation);

        return trade;
    }
}