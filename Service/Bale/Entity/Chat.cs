namespace Bale;

public class Chat
{
    public long Id { get; init; }
    public string Type { get; init; } = null!;
    public string? Title { get; init; }
    public string? Username { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}