using AutoMapper;
using Domain.Entitis;
using Domain.Models;

namespace Infractructure.Configurations;

public class MapperConfiguration :Profile
{
    public MapperConfiguration()
    {
        CreateMap<GetWeatherResponse, Measurement>()
            .ForPath(r => r.LocationEntity.Latitude, m => m.MapFrom(me => me.Latitude))
            .ForPath(r => r.LocationEntity.Longitude, m => m.MapFrom(me => me.Longitude));
    }
}