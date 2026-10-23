using App.Feature.Domain;

namespace App.Feature;

public record TradeCreateCommand(long UserId, Side Side,int Geram,decimal Price) : ICommand<Trade>;

public class TradeCreateCommandHandler(IServiceScopeFactory factory) : ICommandHandler<TradeCreateCommand, Trade>
{
    public async Task<Trade> ExecuteAsync(TradeCreateCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var trade = new Trade(command.UserId, command.Side,command.Geram,command.Price);
        db.Set<Trade>().Add(trade);
        await db.SaveChangesAsync(cancellation);

        return trade;
    }
}