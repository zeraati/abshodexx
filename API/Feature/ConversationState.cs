using System.Collections.Concurrent;

namespace Bale;

public class ConversationState
{
    public ConversationState()
    {
        Flow = Flow.None;
        Step = Step.None;
    }

    public void Update(Flow flow, Step step, long? geram)
    {
        Flow = flow;
        Step = step;
        Geram = geram;
    }

    public void Clear()
    {
        Flow = Flow.None;
        Step = Step.None;
    }

    public Flow Flow { get; private set; }
    public Step Step { get; private set; }
    public long? Geram { get; private set; }
}


public class ConversationStateStore
{
    private readonly ConcurrentDictionary<long, ConversationState> _states = new();
    public ConversationState Get(Update update) =>
        _states.GetOrAdd(update.Message!.Chat.Id, static _ => new ConversationState());

    public bool Clear(Update update) => _states.TryRemove(update.Message!.Chat.Id, out _);
}