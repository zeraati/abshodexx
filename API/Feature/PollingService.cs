using Bale;

namespace API;

public sealed class PollingService(Service baleService, ILogger<PollingService> logger, Dispatcher dispatcher) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        long offset = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            try
            {
                var updates = await baleService.GetUpdates(offset, stoppingToken);
                foreach (var update in updates)
                {
                    if (update.HasMessageText == false) continue;

                    var reply = await dispatcher.Dispatch(update, stoppingToken);
                    var sent = await baleService.SendMessage(update, reply, [Button.Price, Button.Sell,Button.Buy],stoppingToken);
                    offset = update.UpdateId + 1;
                }

            }
            catch (Exception exception) { logger.LogError(exception.Message); }
        }
    }
}
