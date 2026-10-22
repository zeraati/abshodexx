namespace Bale;

public class Dispatcher
{
    private readonly Dictionary<Button, ICommandHandler> _commadns;
    private readonly Dictionary<Flow, IFlowHandler> _flows;
    private static ConversationStateStore _stateStore = new ConversationStateStore();
    public Dispatcher(IEnumerable<ICommandHandler> buttons, IEnumerable<IFlowHandler> flows)
    {
        _commadns = buttons.ToDictionary(x => x.Button);
        _flows = flows.ToDictionary(x => x.Flow);
    }

    public async Task<string> Dispatch(Update update, CancellationToken cancellationToken)
    {
        var stateStore = _stateStore.Get(update);

        // First: check for commands
        var selectedButton = update.Message!.SelectedButton<Button>();
        if (selectedButton.HasValue && _commadns.TryGetValue(selectedButton.Value, out var command))
        {
            return command.Handle(update, stateStore);
        }

        // Second: check the active flow
        if (stateStore.Flow != Flow.None && _flows.TryGetValue(stateStore.Flow, out var flow)) return flow.Handle(update, stateStore);

        return "برای استفاده از بات دکمه های زیر را انتخاب کنید.";
    }
}