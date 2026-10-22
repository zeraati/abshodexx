using API.Feature.Domain;

namespace API.Feature;

public record TradeSetLotCommand(Side Side,int Lot,int IndicativePrice) : ICommand<Trade>;

public class TradeSetLotCommandHandler(IServiceScopeFactory factory) : ICommandHandler<TradeSetLotCommand, Trade>
{
    public async Task<Trade> ExecuteAsync(TradeSetLotCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var trade =await db.Set<Trade>().OrderByDescending(x=>x.Id).FirstAsync(x=>x.Side==command.Side && x.GeramAt==null, cancellation);
        trade.SetLot(command.Lot,command.IndicativePrice);
        db.Set<Trade>().Update(trade);
        await db.SaveChangesAsync(cancellation);

        return trade;
    }
}