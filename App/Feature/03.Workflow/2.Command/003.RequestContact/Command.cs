using Bale;

namespace App.Feature;

public class RequestContactCommandHandler : IWorkflowCommandHandler
{
    public Button Button => Button.RequestContact;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        //var mobile = update.EffectiveMessage!.Contact?.PhoneNumber;
        var message = new SendMessageRequest(update, "برای استفاده از بات ثبت شماره موبایل الزامی است.");
        message.ReplyMarkup = ReplyMarkup.RequestContact;
        return message;
    }
}