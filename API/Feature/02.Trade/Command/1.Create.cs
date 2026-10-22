using API.Feature.Domain;

namespace API.Feature;

public record TradeCreateCommand(long UserId, Side Side) : ICommand<Trade>;
public class TradeCreateCommandHandler(AppDbContext db) : ICommandHandler<TradeCreateCommand, Trade>
{
    public async Task<Trade> ExecuteAsync(TradeCreateCommand command, CancellationToken cancellation)
    {
        var trade = new Trade(command.UserId, command.Side);
        db.Set<Trade>().Add(trade);
        await db.SaveChangesAsync(cancellation);

        return trade;
    }
}