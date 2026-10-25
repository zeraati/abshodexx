using Bale;
using App.Feature;
using Common.Extension;

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
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                await using var scope = factory.CreateAsyncScope();
                var baleService = scope.ServiceProvider.GetRequiredService<BaleService>();
                var dispatcher = scope.ServiceProvider.GetRequiredService<WorkflowDispatcher>();

                var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                var userStateService = scope.ServiceProvider.GetRequiredService<UserStateService>();


                var updates = await baleService.GetUpdates(offset, stoppingToken);
                foreach (var update in updates)
                {
                    offset = update.UpdateId + 1;

                    var flow =await GetFlow(update,userService,userStateService,stoppingToken);
                    var message = await dispatcher.Dispatch(update, flow, stoppingToken);
                    if (message.ReplyMarkup == null) message.AddKeyboard([Button.Price, Button.Sell, Button.Buy]);
                    await baleService.SendMessage(message, stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception) { logger.LogError(exception, "Error while polling updates"); }
        }
    }

    private async Task<Flow?> GetFlow(Update update, UserService userService,UserStateService userStateService,CancellationToken cancellation)
    {
        if (update.Username == null) return Flow.SetUsername;

        var user = await userService.Get(update, cancellation);
        if (user == null) return Flow.CreateUser;

        user = await userService.Get(update, cancellation);
        if (user!.Mobile.Empty()) return Flow.SetMobile;

        return (await userStateService.Get(update.Username!, cancellation))?.Flow;
    }
}