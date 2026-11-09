namespace Bale;
using Common.Extension;

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


    private object? _button { get; set; }
    public void SetButton<TButton>(TButton button) => _button = button;
    public TButton? SelectedButton<TButton>() where TButton :struct, Enum
    {
        if (_button != null) return (TButton)_button;

        if (Text.Empty()) return null;
        var buttonText = Text!.Replace("/start","start");

        if (Enum.TryParse<TButton>(buttonText, true, out var button)) return button;

        return null;
    }

    private object? _flow { get; set; }
    public void SetFlow<TFlow>(TFlow? flow) => _flow = flow;
    public TFlow? Flow<TFlow>() where TFlow : struct, Enum => (TFlow?)_flow;
}

