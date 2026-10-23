using App.Feature.Domain;

namespace App.Feature;

public record GoldPriceCreateCommand(string Symbol, decimal Sell, decimal Buy, DateTime LastUpdate) : ICommand<bool>;

public class GoldPriceCreateCommandHandler(IServiceScopeFactory factory) : ICommandHandler<GoldPriceCreateCommand, bool>
{
    public async Task<bool> ExecuteAsync(GoldPriceCreateCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var exists = await db.Set<GoldPrice>().AnyAsync(x => x.Symbol == command.Symbol && x.LastUpdate == command.LastUpdate, cancellation);
        if (exists) return false;

        db.Set<GoldPrice>().Add(new GoldPrice(command.Symbol, command.Sell, command.Buy, command.LastUpdate));
        await db.SaveChangesAsync(cancellation);

        return true;
    }
}
