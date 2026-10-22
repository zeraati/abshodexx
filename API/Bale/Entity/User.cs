namespace Bale;

public class BaleUser
{
    public long Id { get; set; }
    public bool IsBot { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Name => $"{FirstName} {LastName}".Trim();

    public string? Username { get; set; }
}

