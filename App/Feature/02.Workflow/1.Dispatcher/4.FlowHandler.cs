namespace App.Feature;

public interface IFlowHandler
{
    Flow Flow { get; }
    Task<string> Handle(Update update, CancellationToken cancellation);
}
