using App.Feature;
using App.Feature.Domain;
using Bale;
using Common.Extension;

namespace App;

public class SellFlowHandler(UserStateService userStateService) : IWorkflowFlowHandler
{
    public Flow Flow => Flow.Sell;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        var message = new SendMessageRequest(update, "وضعیت فروش نامعتبر است.");

        var state = await userStateService.Get(update.Username!, cancellation);
        if (state?.Step == Step.WaitingForEnterGeram) message = await HandleGeram(update, cancellation);
        if (state?.Step == Step.WaitingForConfirmOrder) message = await HandleConfirmation(update, cancellation);

        return message;
    }

    private async Task<SendMessageRequest> HandleGeram(Update update, CancellationToken cancellation)
    {
        var geram = (int?)update.Message!.Number;
        if (geram.GetValueOrDefault() <= 0)
            return new SendMessageRequest(update, "مقدار نامتعبر است، لطقا مقدار معتبری وارد نمایید");

        var newState = new UserStateCreate(update.Username!, Flow, Step.WaitingForConfirmOrder, geram, 1000);
        await userStateService.Create(newState, cancellation);

        var message = new SendMessageRequest(update, $"فروش {geram} گرم به قیمت ۱۰۰۰ تومان\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید");
        message.ReplyMarkup = new ReplyMarkup([[InlineKeyboard.Confirm(), InlineKeyboard.Reject()]]);
        var a = Json.Serialize(message);
        return message;
    }

    private async Task<SendMessageRequest> HandleConfirmation(Update update, CancellationToken cancellation)
    {
        var confirmed = update.Message!.Text.ToPersianNumber() == "۱";

        var state = await userStateService.Get(update.Username!, cancellation);
        var newState = new UserStateCreate(update.Username!, Flow, Step.Completed, state!.Geram, 1000);
        await userStateService.Create(newState, cancellation);

        return new SendMessageRequest(update, "مقدار نامتعبر است، لطقا مقدار معتبری وارد نمایید");
        //return confirmed? $"{state!.Geram} گرم خریداری شد.":"درخواست خرید تایید نشد.";
    }
}