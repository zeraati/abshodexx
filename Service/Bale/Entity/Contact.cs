namespace Bale;

public sealed class Contact
{
    public string PhoneNumber { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
    public int? UserId { get; init; }
}