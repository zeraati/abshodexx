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
                    await baleService.RemoveKeyboard(update, stoppingToken);

                    if (update.Username == null)
                    {
                        await baleService.SendMessage(update, "لطفاً در پیام‌رسان «بله» برای حساب خود نام کاربری ثبت کنید", stoppingToken);
                        continue;
                    }

                    var user = await userService.Get(update, stoppingToken);
                    if (user == null)
                    {
                        await userService.Create(update, stoppingToken);
                        continue;
                    }

                    if (user!.Mobile.Empty())
                    {
                        var mobile = update.Message!.Contact?.PhoneNumber;
                        if (mobile.Empty() == true)
                        {
                            await baleService.RequestContact(update, "برای تکمیل ثبت نام شماره موبایل خود را ارسال کنید", stoppingToken);
                            continue;
                        }

                        await userService.UpdateMobile(update.Username!, mobile!, stoppingToken);
                    }

                    var flow = (await userStateService.Get(update.Username!, stoppingToken))?.Flow;
                    var message = await dispatcher.Dispatch(update, flow, stoppingToken);
                    if (message.ReplyMarkup == null) message.AddKeyboard([Button.Price, Button.Sell, Button.Buy]);
                    await baleService.SendMessage(message, stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception) { logger.LogError(exception, "Error while polling updates"); }
        }
    }
}