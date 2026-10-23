namespace Common.Entity;
public class BaseEntity
{
    public long Id { get; set; }
    public DateTime ModifiedAt { get; protected set; } =DateTime.Now;
}