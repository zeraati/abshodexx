using API.Feature;
using FastEndpoints;

namespace Bale;

public class Dispatcher
{
    private readonly Dictionary<Button, ICommandHandler> _commadns;
    private readonly Dictionary<Flow, IFlowHandler> _flows;
    private readonly IServiceProvider _services;

    public Dispatcher(IEnumerable<ICommandHandler> buttons, IEnumerable<IFlowHandler> flows, IServiceProvider services)
    {
        _commadns = buttons.ToDictionary(x => x.Button);
        _flows = flows.ToDictionary(x => x.Flow);
        _services = services;
    }

    // Resolves the handler from this (scoped) provider rather than FastEndpoints' root provider,
    // so scoped dependencies such as AppDbContext can be resolved.
    private async Task<TResult> Execute<TResult>(ICommand<TResult> command, CancellationToken cancellation)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        dynamic handler = _services.GetRequiredService(handlerType);
        return await handler.ExecuteAsync((dynamic)command, cancellation);
    }

    public async Task<string> Dispatch(Update update, CancellationToken cancellation)
    {
        var from = update.Message!.From!;
        var user = await Execute(new UserCreateCommand(from.Name!, from.Username!, null), cancellation);

        // First: check for commands
        var selectedButton = update.Message!.SelectedButton<Button>();
        if (selectedButton.HasValue && _commadns.TryGetValue(selectedButton.Value, out var command))
        {
            await Execute(new UserStateCommandCompletedCommand(user.Id), cancellation);
            return await command.Handle(user.Id,update, cancellation);
        }


        // Second: check the active flow
        var userState = await Execute(new UserStateGetLastCommand(user.Id,null), cancellation);
        if (userState !=null && _flows.TryGetValue(userState.Flow, out var flow)) return await flow.Handle(userState,update, cancellation);

        return "برای استفاده از بات دکمه های زیر را انتخاب کنید.";
    }
}