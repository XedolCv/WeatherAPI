using Domain.Models;

namespace Infractructure.Interfaces;

public interface IWeatherService
{
    Task<GetWeatherResponse> GetWeatherAsync(GetWeatherRequest request);
}