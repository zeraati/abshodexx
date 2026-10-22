namespace Bale;

public interface IFlowHandler
{
    Flow Flow { get; }
    string Handle(Update update, ConversationState state);
}
