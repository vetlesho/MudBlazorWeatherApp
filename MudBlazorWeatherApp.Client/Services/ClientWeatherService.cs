using System.Text.Json;
using MudBlazorWeatherApp.Client.Models;
 

namespace MudBlazorWeatherApp.Client.Services
{
    public class ClientWeatherService
    {
        private readonly HttpClient _httpClient;

        public ClientWeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse?> GetWeatherByCityAsync(string cityName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/weather/{cityName}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonString}");

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return JsonSerializer.Deserialize<WeatherResponse>(jsonString, options);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }
    }
}