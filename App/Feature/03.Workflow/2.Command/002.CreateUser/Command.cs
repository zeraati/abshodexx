using Bale;

namespace App.Feature;

public class CreateUserCommandHandler(UserService userService) : IWorkflowCommandHandler
{
    public Button Button => Button.CreateUser;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        await userService.Create(update, cancellation);
        return new SendMessageRequest(update, "حساب کاربری با موفقیت ایجاد شد.");
    }
}