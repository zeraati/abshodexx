using Bale;
using App.Feature;

namespace App;

public class SetUsernameFlowHandler(BaleService baleService) : IWorkflowFlowHandler
{
    public Flow Flow => Flow.SetUsername;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        return new SendMessageRequest(update, "لطفاً در پیام‌رسان «بله» برای حساب خود نام کاربری ثبت کنید");
    }
}