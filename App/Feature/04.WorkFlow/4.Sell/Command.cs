using App.Feature;
using Bale;
namespace App;

public class SellCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Sell;
    public async Task<string> Handle(long userId, Update update, CancellationToken cancellation)
    {
        await new UserStateCreateCommand(userId, Flow.Sell, Step.WaitingForEnterGeram, null, null).ExecuteAsync(cancellation);
        return "چند گرم می‌خواهید بفروشید؟";
    }
}
