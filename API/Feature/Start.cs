using Bale;
namespace API;

public class StartCommandHandler : ICommandHandler
{
    public Button Button => Button.Start;

    public string Handle(Update update,ConversationState state)
    {
        state.Clear();
        return "خوش آمدید! برای استفاده از بات دکمه های زیر را انتخاب کنید.";
    }
}
