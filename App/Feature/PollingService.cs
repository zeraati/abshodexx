using Bale;

namespace App;

public sealed class PollingService(IServiceScopeFactory factory, ILogger<PollingService> logger) : BackgroundService, IHosted
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        long offset = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);

                await using var scope = factory.CreateAsyncScope();
                var baleService = scope.ServiceProvider.GetRequiredService<BaleService>();
                var dispatcher = scope.ServiceProvider.GetRequiredService<Dispatcher>();

                var updates = await baleService.GetUpdates(offset, stoppingToken);
                foreach (var update in updates)
                {
                    offset = update.UpdateId + 1;

                    if (update.HasMessageText == false) continue;

                    var reply = await dispatcher.Dispatch(update, stoppingToken);
                    await baleService.SendMessage(update, reply, [Button.Price, Button.Buy, Button.Sell], stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception) { logger.LogError(exception, "Error while polling updates"); }
        }
    }
}