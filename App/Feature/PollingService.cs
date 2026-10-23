using Bale;
using App.Feature;

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
                var userStateService = scope.ServiceProvider.GetRequiredService<UserStateService>();
                var dispatcher = scope.ServiceProvider.GetRequiredService<WorkflowDispatcher>();

                var updates = await baleService.GetUpdates(offset, stoppingToken);
                foreach (var update in updates)
                {
                    offset = update.UpdateId + 1;
                    if (update.Username == null)
                        await baleService.SendMessage(update, "لطفاً در پیام‌رسان «بله» برای حساب خود نام کاربری ثبت کنید", stoppingToken);

                    if (update.HasMessageText == false) continue;

                    var flow = (await userStateService.Get(update.Username!, stoppingToken))?.Flow;
                    var reply = await dispatcher.Dispatch(update, flow, stoppingToken);
                    await baleService.SendMessage(update, reply, [Button.Price, Button.Buy, Button.Sell], stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception) { logger.LogError(exception, "Error while polling updates"); }
        }
    }
}