using Bale;

namespace API.Feature;

public class BuyCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Buy;

    public async Task<string> Handle(long userId,Update update,CancellationToken cancellation)
    {
        await new UserStateCreateCommand(userId,Flow.Buy, Step.WaitingForEnterGeram, null).ExecuteAsync(cancellation);
        return "چند گرم می‌خواهید بخرید؟";
    }
}
