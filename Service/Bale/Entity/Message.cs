using Common;
using Common.Extension;
namespace Bale;

public class Message
{
    public long MessageId { get; set; }

    public BaleUser? From { get; set; }

    public long Date { get; set; }

    public Chat Chat { get; set; } = null!;

    public Contact? Contact { get; set; }

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

