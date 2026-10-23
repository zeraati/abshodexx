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

    public async Task<string> Dispatch(Update update, CancellationToken cancellation)
    {
        // first: command
        var selectedButton = update.Message!.SelectedButton<Button>();
        if (selectedButton.HasValue && _commadns.TryGetValue(selectedButton.Value, out var command))
        {
            return await command.Handle(update, cancellation);
        }


        // second: flow
        //var userState = await new UserStateGetLastCommand(user.Id,null).ExecuteAsync(cancellation);
        //if (userState !=null && _flows.TryGetValue(userState.Flow, out var flow)) return await flow.Handle(userState,update, cancellation);

        return "برای استفاده از بات دکمه های زیر را انتخاب کنید.";
    }
}