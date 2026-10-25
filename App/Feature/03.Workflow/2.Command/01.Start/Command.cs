using Bale;

namespace App.Feature;

public class StartCommandHandler(UserStateService userStateService): IWorkflowCommandHandler
{
    public Button Button => Button.Start;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        await userStateService.Create(new UserStateCreate(update.Username!, Flow.Start, Step.Completed,null,null), cancellation);

        var buy = new InlineKeyboard("➕ خرید", Button.Buy.ToString());
        var sell = new InlineKeyboard("➖ فروش", Button.Sell.ToString());
        var price = new InlineKeyboard("💰 قیمت", Button.Price.ToString());

        var message = new SendMessageRequest(update,"لطفاً یکی از گزینه‌ها را انتخاب کنید:");
        message.ReplyMarkup = new ReplyMarkup([[price],[buy, sell]]);
        return message;
    }
}