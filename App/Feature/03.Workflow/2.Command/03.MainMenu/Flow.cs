using Bale;
using App.Feature;

namespace App;

public class MainMenuFlowHandler : IWorkflowFlowHandler
{
    public Flow Flow => Flow.MainMenu;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        var buy=new InlineKeyboard("➕ خرید", Button.Buy.ToString());
        var sell=new InlineKeyboard("➖ فروش", Button.Sell.ToString());
        var price=new InlineKeyboard("💰 قیمت", Button.Price.ToString());

        var message = new SendMessageRequest(update, null);
        message.ReplyMarkup=new ReplyMarkup([[buy,sell,price]]);

        return message;
    }
}