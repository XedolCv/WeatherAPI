namespace Domain.Models;

public class GetWeatherResponse
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public DateTime Time { get; set; }
}