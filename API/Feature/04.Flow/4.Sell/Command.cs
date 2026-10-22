using Bale;
namespace API;

public class SellCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Sell;
    public Task<string> Handle(Update update, CancellationToken cancellation)
    {
        state.Update(Flow.Sell, Step.WaitingForEnterGeram, null);
        return "چند گرم می‌خواهید بفروشید؟";
    }
}
