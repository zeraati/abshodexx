using Bale;

namespace App.Feature;

public class WorkflowDispatcher : IScoped
{
    private readonly Dictionary<Button, IWorkflowCommandHandler> _commadns;
    private readonly Dictionary<Flow, IWorkflowFlowHandler> _flows;

    public WorkflowDispatcher(IEnumerable<IWorkflowCommandHandler> buttons, IEnumerable<IWorkflowFlowHandler> flows)
    {
        _commadns = buttons.ToDictionary(x => x.Button);
        _flows = flows.ToDictionary(x => x.Flow);
    }

    public async Task<SendMessageRequest> Dispatch(Update update, CancellationToken cancellation)
    {
        // first: command
        var selectedButton = update.EffectiveMessage!.SelectedButton<Button>();
        if (selectedButton.HasValue && _commadns.TryGetValue(selectedButton.Value, out var commandHandler))
            return await commandHandler.Handle(update, cancellation);


        // second: flow
        var flow = update.EffectiveMessage!.Flow<Flow>();
        if (flow != null && _flows.TryGetValue(flow.Value, out var flowHandler))
            return await flowHandler.Handle(update, cancellation);


        return new SendMessageRequest(update, "برای استفاده از بات دکمه های زیر را انتخاب کنید.");
    }
}