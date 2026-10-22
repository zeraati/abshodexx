using Bale;
namespace API;

public class SellCommandHandler : ICommandHandler
{
    public Button Button => Button.Sell;
    public string Handle(Update update,ConversationState state)
    {
        state.Update(Flow.Sell, Step.WaitingForEnterGeram, null);
        return "چند گرم می‌خواهید بفروشید؟";
    }
}
