namespace Domain.Entitis;

public class Measurement:BaseEntity
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public Location LocationEntity { get; set; } = new Location();
    public Guid LocationEntityId { get; set; }
}