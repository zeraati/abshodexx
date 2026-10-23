using Bale;
namespace App;
using App.Feature;

public class StartCommandHandler : Bale.ICommandHandler
{
    public Button Button => Button.Start;

    public async Task<string> Handle(long userId, Update update,CancellationToken cancellation)
    {
        await new UserStateCommandCompletedCommand(userId).ExecuteAsync(cancellation);
        return "خوش آمدید! برای استفاده از بات دکمه های زیر را انتخاب کنید.";
    }
}
