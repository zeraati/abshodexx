using API;

namespace Bale;

public interface CommandHandler
{
    Button Button { get; }
    string Handle(Update update,ConversationState state);
}
