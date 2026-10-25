using Bale;
namespace App;
using App.Feature;


public class StartCommandHandler: IWorkflowCommandHandler
{
    public Button Button => Button.Start;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        //await new UserStateCommandCompletedCommand(userId).ExecuteAsync(cancellation);
        var buy = new InlineKeyboard("➕ خرید", Button.Buy.ToString());
        var sell = new InlineKeyboard("➖ فروش", Button.Sell.ToString());
        var price = new InlineKeyboard("💰 قیمت", Button.Price.ToString());

        var message = new SendMessageRequest(update, null);
        message.ReplyMarkup = new ReplyMarkup([[buy, sell, price]]);

        return message;
    }
}