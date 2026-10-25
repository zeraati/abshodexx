using Bale;
using App.Feature;

namespace App;

public class CreateUserFlowHandler(UserService userService) : IWorkflowFlowHandler
{
    public Flow Flow => Flow.CreateUser;

    public async Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation)
    {
        await userService.Create(update, cancellation);
        return new SendMessageRequest(update, "حساب کاربری با موفقیت ایجاد شد.");
    }
}