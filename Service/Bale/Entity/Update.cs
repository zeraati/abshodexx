using Common.Extension;

namespace Bale;

public class Update
{
    public long UpdateId { get; init; }
    public Message? Message { get; init; }
    public Message? EditedMessage { get; init; }
    public CallbackQuery? CallbackQuery { get; init; }
    public PreCheckoutQuery? PreCheckoutQuery { get; init; }

    public Message? EffectiveMessage => Message ?? CallbackQuery?.Message;
    public string? Username => EffectiveMessage?.From?.Username;
    public bool HasMessageText => EffectiveMessage?.Text.Empty() == false;
}