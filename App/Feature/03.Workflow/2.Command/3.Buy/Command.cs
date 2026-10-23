using Bale;

namespace App.Feature;

public class BuyCommandHandler : IWorkflowCommandHandler
{
    public Button Button => Button.Buy;

    public async Task<string> Handle(Update update,CancellationToken cancellation)
    {
        //await new UserStateCreateCommand(userId,Flow.Buy, Step.WaitingForEnterGeram, null,null).ExecuteAsync(cancellation);
        return "چند گرم می‌خواهید بخرید؟";
    }
}
