using API.Feature.Domain;

namespace Bale;

public interface IFlowHandler
{
    Flow Flow { get; }
    Task<string> Handle(UserState userState, Update update, CancellationToken cancellation);
}
