using Bale;

namespace App.Feature;

public class SetUsernameCommandHandler: IWorkflowCommandHandler
{
    public Button Button => Button.SetUsername;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        return new SendMessageRequest(update, "لطفاً در پیام‌رسان «بله» برای حساب خود نام کاربری ثبت کنید");
    }
}