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

                    await SetCommandAndFlow(update, userService, userStateService, stoppingToken);
                    var message = await dispatcher.Dispatch(update, stoppingToken);
                    await baleService.SendMessage(message, stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception) { logger.LogError(exception, "Error while polling updates"); }
        }
    }

    private static async Task SetCommandAndFlow(Update update, UserService userService, UserStateService userStateService, CancellationToken cancellation)
    {

        if (update.HasMessageText == false)
        {
            update.EffectiveMessage!.SetButton(Button.Start);
            update.EffectiveMessage!.SetFlow<Flow?>(null);
            return;
        }

        if (update.Username == null)
        {
            update.EffectiveMessage!.SetButton(Button.SetUsername);
            update.EffectiveMessage!.SetFlow<Flow?>(null);
            return;
        }

        var user = await userService.Get(update, cancellation);
        if (user == null)
        {
            update.EffectiveMessage!.SetButton(Button.CreateUser);
            update.EffectiveMessage!.SetFlow<Flow?>(null);
            return;
        }

        var phoneNumber = update.EffectiveMessage?.Contact?.PhoneNumber;
        if (user!.Mobile.Empty() && phoneNumber.Empty())
        {
            update.EffectiveMessage!.SetButton(Button.RequestContact);
            update.EffectiveMessage!.SetFlow<Flow?>(null);
            return;
        }

        if (user!.Mobile.Empty() && phoneNumber.Empty()==false)
        {
            update.EffectiveMessage!.SetButton(Button.SetMobile);
            update.EffectiveMessage!.SetFlow<Flow?>(null);
            return;
        }

        var userStateFlow = (await userStateService.Get(update.Username!, cancellation))?.Flow;
        if (userStateFlow == null)
        {
            update.EffectiveMessage!.SetButton(Button.Start);
            update.EffectiveMessage!.SetFlow<Flow?>(null);
            return;
        }
    }
}