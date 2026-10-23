namespace Bale;

public interface ICommandHandler
{
    Button Button { get; }
    Task<string> Handle(Update update,CancellationToken cancellation);
}
