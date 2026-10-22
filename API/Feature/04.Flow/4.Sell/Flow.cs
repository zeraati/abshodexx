using API.Feature;
using Bale;
using Common.Extension;

namespace API;

public class SellFlowHandler : IFlowHandler
{
    public Flow Flow => Flow.Sell;

    public async Task<string> Handle(Update update, CancellationToken cancellation)
    {
        var from = update.Message!.From!;
        var user = await new UserCreateCommand(from.Name!, from.Username!, null).ExecuteAsync(cancellation);
        var userState = await new UserStateGetLastCommand(user.Id, Flow).ExecuteAsync(cancellation);

        var message = "وضعیت فروش نامعتبر است.";

        if (userState?.Step == Step.WaitingForEnterGeram) message = await HandleGeram(update, user.Id, cancellation);
        if (userState?.Step == Step.WaitingForConfirmOrder) message = await HandleConfirmation(update, user.Id, cancellation);

        return message;
    }

    private async Task<string> HandleGeram(Update update, long userId, CancellationToken cancellation)
    {
        var geram = (int?)update.Message!.Number;
        if (geram.GetValueOrDefault() <= 0) return "مقدار نامتعبر است، لطقا مقدار معتبری وارد نمایید";

        await new UserStateCreateCommand(userId, Flow, Step.WaitingForConfirmOrder, geram).ExecuteAsync(cancellation);
        return $"فروش {geram} گرم\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید";
    }

    private async Task<string> HandleConfirmation(Update update, long userId, CancellationToken cancellation)
    {
        var confirmed = update.Message!.Text.ToPersianNumber() == "۱";
        var userState = await new UserStateStepCompletedCommand(userId, Flow, confirmed).ExecuteAsync(cancellation);

        return confirmed ? $"{userState!.Geram} گرم فروخته شد." : "درخواست فروش تایید نشد.";
    }
}