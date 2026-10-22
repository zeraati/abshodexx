using Bale;
namespace API;

public class StartCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Start;

    public Task<string> Handle(Update update,CancellationToken cancellation)
    {
        //state.Clear();
        return Task.FromResult("خوش آمدید! برای استفاده از بات دکمه های زیر را انتخاب کنید.");
    }
}
