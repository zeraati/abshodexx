using Bale;

namespace API.Feature;

public class BuyCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Buy;

    public async Task<string> Handle(Update update,CancellationToken cancellation)
    {
        var from = update.Message!.From!;
        var user = await new UserCreateCommand(from.Name!, from.Username!, null).ExecuteAsync(cancellation);

        state.Update(Flow.Buy, Step.WaitingForEnterGeram, null);
        return "چند گرم می‌خواهید بخرید؟";
    }
}
