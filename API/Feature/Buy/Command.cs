using Bale;

namespace API;

public class BuyCommandHandler : ICommandHandler
{
    public Button Button => Button.Buy;

    public string Handle(Update update, ConversationState state)
    {
        state.Update(Flow.Buy, Step.WaitingForEnterGeram, null);
        return "چند گرم می‌خواهید بخرید؟";
    }
}
