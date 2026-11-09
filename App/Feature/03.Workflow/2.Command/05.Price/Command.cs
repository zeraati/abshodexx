using Bale;
using Baha24;
namespace App.Feature;
using App.Feature;
using Common.Util;

public class PriceCommandHandler(Baha24Service service) : IWorkflowCommandHandler
{
    public Button Button => Button.Price;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        //await new UserStateCommandCompletedCommand(userId).ExecuteAsync(cancellation);
        var price = await service.GetPriceGold18(cancellation);
        return new SendMessageRequest(update,$"خرید {Number.ToToman(price!.Buy)}\nفروش {Number.ToToman(price!.Sell)}");
    }
}
