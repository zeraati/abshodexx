using Bale;
namespace App;
using App.Feature;
using Common.Util;

public class PriceCommandHandler (Baha24.Service service) : Bale.ICommandHandler
{
    public Button Button => Button.Price;

    public async Task<string> Handle(long userId, Update update, CancellationToken cancellation)
    {
        await new UserStateCommandCompletedCommand(userId).ExecuteAsync(cancellation);
        var price=await service.GetPriceGold18(cancellation);
        return $"خرید {Number.ToToman(price!.Buy)}\nفروش {Number.ToToman(price!.Sell)}";
    }
}
