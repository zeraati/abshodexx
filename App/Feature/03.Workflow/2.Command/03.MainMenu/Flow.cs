using Bale;
using App.Feature;

namespace App;

public class MainMenuFlowHandler: IWorkflowFlowHandler
{
    public Flow Flow => Flow.MainMenu;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {

        var message = new SendMessageRequest;
        if (message.ReplyMarkup == null) message.AddKeyboard([Button.Price, Button.Sell, Button.Buy]);

        return new SendMessageRequest(update, "لطفاً در پیام‌رسان «بله» برای حساب خود نام کاربری ثبت کنید");
    }
}