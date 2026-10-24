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
        var resourceKey = MessageResource.ResourceManager.ResourceKey(Text?.Trim());
        if (resourceKey.Empty()) return null;

        if (Enum.TryParse<TButton>(resourceKey!.Replace("start", "/start"), true, out var button)) return button;

        return null;
    }
}

