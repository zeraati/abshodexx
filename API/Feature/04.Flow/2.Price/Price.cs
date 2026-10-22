using Bale;
namespace API;

public class PriceCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Price;

    public Task<string> Handle(Update update, CancellationToken cancellation)
    {
        //state.Clear();
        return Task.FromResult("خرید از ما ۱۰۰ تومان\nفروش به ما ۹۰ تومان");
    }
}
