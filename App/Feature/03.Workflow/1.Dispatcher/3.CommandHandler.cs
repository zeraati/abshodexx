using Bale;

namespace App.Feature;

public interface IWorkflowCommandHandler
{
    Button Button { get; }
    Task<string> Handle(Update update,CancellationToken cancellation);
}
