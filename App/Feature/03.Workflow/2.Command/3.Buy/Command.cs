using Bale;

namespace App.Feature;

public class BuyCommandHandler(UserStateService userStateService) : IWorkflowCommandHandler
{
    public Button Button => Button.Buy;

    public async Task<string> Handle(Update update,CancellationToken cancellation)
    {
        var userState = new UserStateCreate(update.Username!, Flow.Buy, Step.WaitingForEnterGeram, null, null);
        await userStateService.Create(userState, cancellation);

        return "چند گرم می‌خواهید بخرید؟";
    }
}
