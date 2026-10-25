using Bale;
using App.Feature;
using Common.Extension;

namespace App.Feature;

public class SetMobileFlowHandler(UserService userService) : IWorkflowFlowHandler
{
    public Flow Flow => Flow.SetMobile;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        var mobile = update.EffectiveMessage!.Contact?.PhoneNumber;
        if (mobile.Empty() == true)
        {
            var message = new SendMessageRequest(update, "شماره موبایل خود را ارسال کنید")
            {
                ReplyMarkup = ReplyMarkup.RequestContact
            };

            return message;
        }

        await userService.UpdateMobile(update.Username!, mobile!, cancellation);
        return new SendMessageRequest(update, "شماره موبایل شما با موفقیت ثبت شد.");
    }
}