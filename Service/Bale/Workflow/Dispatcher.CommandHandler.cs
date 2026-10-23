namespace Bale;

public interface ICommandHandler
{
    Button Button { get; }
    Task<string> Handle(long userId,Update update,CancellationToken cancellation);
}
