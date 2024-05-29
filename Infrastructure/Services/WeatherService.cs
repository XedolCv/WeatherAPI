using AutoMapper;
using Domain;
using Domain.Entitis;
using Domain.Models;
using Infractructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infractructure.Services;

public class WeatherService : IWeatherService
{
    private readonly IRepository<WeatherContext> _repository;
    private readonly IMapper _mapper;

    public WeatherService(IRepository<WeatherContext> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetWeatherResponse> GetWeatherAsync(GetWeatherRequest request)
    {
        try
        {
            var res = await _repository
                .Get<Location>(m => m.Longitude == request.Longitude && m.Latitude == request.Latitude)
                .Join(_repository.Context.Measurements, l => l.Id, m => m.LocationEntityId,
                    (l, m) => new GetWeatherResponse
                    {
                        Longitude = l.Longitude, Latitude = l.Latitude, Temperature = m.Temperature,
                        WindSpeed = m.WindSpeed, Time = m.Time
                    })
                .FirstOrDefaultAsync(m => m.Time >= DateTime.UtcNow - new TimeSpan(0, 10, 0));
            if (res != null)
            {
                return res;
            }

            var rnd = new Random();
            var resp = new GetWeatherResponse
            {
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                Temperature = rnd.Next(30, 35),
                WindSpeed = rnd.Next(1, 10),
                Time = DateTime.UtcNow
            };
            //await _repository.AddAsync(_mapper.Map<Measurement>(resp));
            //await _repository.SaveChangesAsync();
            return resp;
        }
        catch (Exception e)
        { 
            Console.WriteLine(e);
            var rnd = new Random();
           return new GetWeatherResponse
           {
               Longitude = request.Longitude,
               Latitude = request.Latitude,
               Temperature = rnd.Next(30, 35),
               WindSpeed = rnd.Next(1, 10),
               Time = DateTime.UtcNow
           };
        }
    }
}