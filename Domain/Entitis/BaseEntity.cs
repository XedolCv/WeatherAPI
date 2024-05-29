namespace Domain.Entitis;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
}