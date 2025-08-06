using System.Globalization;
using System.Text;
using System.Text.Json;
using MudBlazorWeatherApp.Client.Models;

namespace MudBlazorWeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.met.no/weatherapi/locationforecast/2.0/compact";
        
        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["WeatherApiKey"] ?? throw new InvalidOperationException("WeatherApiKey not found in configuration");
        }

        public (double lat, double lon)? GetCoordinatesForCity(string cityName)
        {
            var coordinates = new Dictionary<string, (double lat, double lon)>
            {
                { "oslo", (59.91, 10.75) },
                { "bergen", (60.39, 5.32) },
                { "trondheim", (63.43, 10.40) },
                { "stavanger", (58.97, 5.73) },
                { "tromsø", (69.65, 18.96) },
                { "new york", (40.71, -74.01) },
                { "london", (51.51, -0.13) },
                { "paris", (48.85, 2.35) },
                { "tokyo", (35.68, 139.69) },
                { "sydney", (-33.87, 151.21) }
            };

            return coordinates.TryGetValue(cityName.ToLower(), out var coords) ? coords : null;
        }

        public async Task<WeatherResponse?> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                var url = $"{BaseUrl}?lat={latitude.ToString("F2", CultureInfo.InvariantCulture)}&lon={longitude.ToString("F2", CultureInfo.InvariantCulture)}";

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "MudBlazorWeatherApp/1.0");

                var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_apiKey}:"));
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authValue);
                
                var response = await _httpClient.GetAsync(url); 
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response received, length: {jsonString.Length}");
                    
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