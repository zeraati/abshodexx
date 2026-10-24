namespace Bale;

public sealed class CallbackQuery
{
    public string Id { get; init; } = null!;
    public BaleUser? From { get; init; }
    public Message? Message { get; init; }
    public string? Data { get; init; }
}
