using Bale;

namespace App.Feature;

public interface IWorkflowFlowHandler
{
    Flow Flow { get; }
    Task<SendMessageRequest> Handle(Update update, CancellationToken cancellation);
}
