using Microsoft.AspNetCore.Mvc;
using MudBlazorWeatherApp.Client.Models;
using MudBlazorWeatherApp.Services;

namespace MudBlazorWeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{cityName}")]
        public async Task<ActionResult<WeatherResponse?>> GetWeather(string cityName)
        {
            Console.WriteLine($"API called for city: {cityName}");

            var coordinates = _weatherService.GetCoordinatesForCity(cityName);
            if (!coordinates.HasValue)
            {
                Console.WriteLine($"City not found: {cityName}");
                return NotFound("City not found");
            }
            Console.WriteLine($"Coordinates found: {coordinates.Value.lat}, {coordinates.Value.lon}");

            var weather = await _weatherService.GetWeatherAsync(coordinates.Value.lat, coordinates.Value.lon);
            if (weather == null)
            {
                Console.WriteLine("Weather data is null");
                return BadRequest("Failed to fetch weather data");
            }
            
            Console.WriteLine("Weather data retrieved successfully");
            return Ok(weather);
        }
    }
}