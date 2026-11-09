using Bale;

namespace App.Feature;

public class SetMobileCommandHandler(UserService userService) : IWorkflowCommandHandler
{
    public Button Button => Button.SetMobile;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        var mobile = update.EffectiveMessage!.Contact?.PhoneNumber;
        await userService.UpdateMobile(update.Username!, mobile!, cancellation);
        return new SendMessageRequest(update, "شماره موبایل شما با موفقیت ثبت شد.");
    }
}