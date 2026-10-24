using Common.Extension;

namespace Bale;

public class Update
{
    public long UpdateId { get; init; }
    public Message? Message { get; init; }
    public Message? EditedMessage { get; init; }
    public CallbackQuery? CallbackQuery { get; init; }
    public PreCheckoutQuery? PreCheckoutQuery { get; init; }

    public string? Username => Message?.From?.Username;
    public bool HasMessageText => Message?.Text.Empty() == false;
}