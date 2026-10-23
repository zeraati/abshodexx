using Bale;
using App.Feature;
using Common.Extension;

namespace App;

public class BuyFlowHandler(UserStateService userStateService) : IWorkflowFlowHandler
{
    public Flow Flow => Flow.Buy;

    public async Task<string> Handle(Update update, CancellationToken cancellation)
    {
        var message = "وضعیت خرید نامعتبر است.";

        var state = await userStateService.Get(update.Username!, cancellation);
        if (state?.Step == Step.WaitingForEnterGeram) message = await HandleGeram(update, cancellation);
        if (state?.Step == Step.WaitingForConfirmOrder) message = await HandleConfirmation(update, cancellation);

        return message;
    }

    private async Task<string> HandleGeram(Update update, CancellationToken cancellation)
    {
        var geram = (int?)update.Message!.Number;
        if (geram.GetValueOrDefault() <= 0) return "مقدار نامتعبر است، لطقا مقدار معتبری وارد نمایید";

        var newState=new UserStateCreate(update.Username!, Flow, Step.WaitingForConfirmOrder, geram, 1000);
        await userStateService.Create(newState, cancellation);
        return $"خرید {geram} گرم به قیمت ۱۰۰۰ تومان\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید";
    }

    private async Task<string> HandleConfirmation(Update update, CancellationToken cancellation)
    {
        var confirmed = update.Message!.Text.ToPersianNumber() == "۱";

        var state = await userStateService.Get(update.Username!, cancellation);
        var newState = new UserStateCreate(update.Username!, Flow, Step.Completed, state!.Geram, 1000);
        await userStateService.Create(newState, cancellation);

        return confirmed? $"{state!.Geram} گرم خریداری شد.":"درخواست خرید تایید نشد.";
    }
}