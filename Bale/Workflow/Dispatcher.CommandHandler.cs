namespace Bale;

public interface ICommandHandler
{
    Button Button { get; }
    string Handle(Update update,ConversationState state);
}
