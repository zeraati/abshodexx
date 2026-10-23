using Baha24;

namespace App.Feature;

public sealed class GoldPriceFetchService(IServiceScopeFactory factory, ILogger<GoldPriceFetchService> logger) : BackgroundService, IHosted
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(30);
    private const int MaxAttempts = 3;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Interval);

        try
        {
            do
            {
                await FetchAndStore(stoppingToken);
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { }
    }

    private async Task FetchAndStore(CancellationToken cancellation)
    {
        try
        {
            await using var scope = factory.CreateAsyncScope();
            var baha24 = scope.ServiceProvider.GetRequiredService<Baha24.Service>();

            var price = await GetPriceWithRetry(baha24, cancellation);
            if (price is null)
            {
                logger.LogWarning("Gold price was not fetched after {MaxAttempts} attempts", MaxAttempts);
                return;
            }

            var stored = await new GoldPriceCreateCommand(price.Symbol, price.Sell, price.Buy, price.LastUpdate).ExecuteAsync(cancellation);
            if (stored)
                logger.LogInformation("Stored gold price {Symbol}: Sell {Sell}, Buy {Buy}, LastUpdate {LastUpdate}", price.Symbol, price.Sell, price.Buy, price.LastUpdate);
        }
        catch (Exception exception) when (cancellation.IsCancellationRequested == false)
        {
            logger.LogError(exception, "Error while fetching and storing gold price");
        }
    }

    private async Task<GetPriceGold18Response?> GetPriceWithRetry(Baha24.Service service, CancellationToken cancellation)
    {
        for (var attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            var price = await service.GetPriceGold18(cancellation);
            if (price is not null) return price;

            logger.LogWarning("Gold price request attempt {Attempt}/{MaxAttempts} failed", attempt, MaxAttempts);
            if (attempt < MaxAttempts) await Task.Delay(RetryDelay, cancellation);
        }

        return null;
    }
}
