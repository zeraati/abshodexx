using App.Feature;

namespace Bale;

public class Dispatcher: IScoped
{
    private readonly Dictionary<Button, ICommandHandler> _commadns;
    private readonly Dictionary<Flow, IFlowHandler> _flows;

    public Dispatcher(IEnumerable<ICommandHandler> buttons, IEnumerable<IFlowHandler> flows)
    {
        _commadns = buttons.ToDictionary(x => x.Button);
        _flows = flows.ToDictionary(x => x.Flow);
    }

    public async Task<string> Dispatch(Update update, CancellationToken cancellation)
    {
        var from = update.Message!.From!;
        var user = await new UserCreateCommand(from.Name!, from.Username!, null).ExecuteAsync(cancellation);

        // First: check for commands
        var selectedButton = update.Message!.SelectedButton<Button>();
        if (selectedButton.HasValue && _commadns.TryGetValue(selectedButton.Value, out var command))
        {
            await new UserStateCommandCompletedCommand(user.Id).ExecuteAsync(cancellation);
            return await command.Handle(user.Id,update, cancellation);
        }


        // Second: check the active flow
        var userState = await new UserStateGetLastCommand(user.Id,null).ExecuteAsync(cancellation);
        if (userState !=null && _flows.TryGetValue(userState.Flow, out var flow)) return await flow.Handle(userState,update, cancellation);

        return "برای استفاده از بات دکمه های زیر را انتخاب کنید.";
    }
}