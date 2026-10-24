using Common;
using Common.Extension;
namespace Bale;

public class Message
{
    public long MessageId { get; init; }
    public BaleUser? From { get; init; }
    public long Date { get; init; }
    public Chat Chat { get; init; } = null!;
    public Contact? Contact { get; init; }

    private string? _text;
    public string? Text
    {
        get => _text;
        set => _text = value?.Trim();
    }
    public long? Number => long.TryParse(Text, out var number) ? number : null;
    public decimal? Decimal => decimal.TryParse(Text, out var number) ? number : null;

    public TButton? SelectedButton<TButton>() where TButton :struct, Enum
    {
        var label = Text?.Trim()?.Replace("➕ ", "").Replace("➖ ", "").Replace("💰 ", "");
        var resourceKey = MessageResource.ResourceManager.ResourceKey(label);
        if (resourceKey.Empty()) return null;

        resourceKey = resourceKey!.Replace("start", "/start");

        if (Enum.TryParse<TButton>(resourceKey, true, out var button)) return button;

        return null;
    }
}

