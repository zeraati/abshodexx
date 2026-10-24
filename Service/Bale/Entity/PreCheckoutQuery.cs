namespace Bale;

public sealed class PreCheckoutQuery
{
    public string Id { get; init; } = null!;
    public BaleUser? From { get; init; }
    public string Currency { get; init; } = null!;
    public int TotalAmount { get; init; }
    public string InvoicePayload { get; init; } = null!;
}