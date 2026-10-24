using Bale;

namespace App.Feature;

public interface IWorkflowCommandHandler
{
    Button Button { get; }
    Task<SendMessageRequest> Handle(Update update,CancellationToken cancellation);
}
