using Common.Entity;

namespace App.Feature.Domain;

public partial class UserState : BaseEntity
{
    private UserState(){ }
    public long UserId { get; set; }

    public Flow Flow { get; private set; }
    public Step Step { get; private set; }
    public int? Geram { get; private set; }
    public decimal? Price { get; private set; }
}


public class UserStateConfig : IEntityTypeConfiguration<UserState>
{
    public void Configure(EntityTypeBuilder<UserState> builder)
    {
    }
}