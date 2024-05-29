using Domain.Models;
using Infractructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    [HttpGet]
    public async Task<IActionResult> GetNewsWithCounterAsync([FromQuery]decimal Latitude,decimal Longitude)
    {
        try
        {
            var res = await _weatherService.GetWeatherAsync(new GetWeatherRequest{Longitude = Longitude,Latitude = Latitude});
            Response.Headers.Append("Access-Control-Allow-Origin", "*");
            return Ok(res);
        }
        catch (Exception e)
        {
            Response.Headers.Append("X-Error-Message",e.Message);
            return BadRequest();
        }
    }
}