using Bale;

namespace App.Feature;

public interface IWorkflowFlowHandler
{
    Flow Flow { get; }
    Task<string> Handle(Update update, CancellationToken cancellation);
}
