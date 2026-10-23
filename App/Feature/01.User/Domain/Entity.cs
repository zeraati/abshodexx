using Common.Entity;

namespace App.Feature.Domain;

public partial class User : BaseEntity
{
    private User() { }
    public string Name { get; private set; } = null!;
    public string Username { get; private set; } = null!;
    public string? Mobile { get; private set; } = null!;
}

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
    }
}