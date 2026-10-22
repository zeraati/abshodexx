using Bale;
namespace API;

public class PriceCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Price;

    public string Handle(Update update, ConversationState state)
    {
        state.Clear();
        return "خرید از ما ۱۰۰ تومان\nفروش به ما ۹۰ تومان";
    }
}
