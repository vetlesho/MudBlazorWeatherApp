using System.Text.Json.Serialization;

// Should probably be in Main Project directory
namespace MudBlazorWeatherApp.Client.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; } = new();

        [JsonPropertyName("properties")]
        public Properties Properties { get; set; } = new();
    }

    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("coordinates")]
        public double[] Coordinates { get; set; } = Array.Empty<double>();
    }

    public class Properties
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; } = new();

        [JsonPropertyName("timeseries")]
        public TimeSeries[] TimeSeries { get; set; } = Array.Empty<TimeSeries>();
    }

    public class Meta
    {
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("units")]
        public Units Units { get; set; } = new();
    }

    public class Units
    {
        [JsonPropertyName("air_temperature")]
        public string AirTemperature { get; set; } = string.Empty;

        [JsonPropertyName("relative_humidity")]
        public string RelativeHumidity { get; set; } = string.Empty;

        [JsonPropertyName("wind_speed")]
        public string WindSpeed { get; set; } = string.Empty;
    }

    public class TimeSeries
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("data")]
        public WeatherData Data { get; set; } = new();
    }

    public class WeatherData
    {
        [JsonPropertyName("instant")]
        public Instant Instant { get; set; } = new();
    }

    public class Instant
    {
        [JsonPropertyName("details")]
        public Details Details { get; set; } = new();
    }

    public class Details
    {
        [JsonPropertyName("air_temperature")]
        public double AirTemperature { get; set; }

        [JsonPropertyName("cloud_area_fraction")]
        public double CloudAreaFraction { get; set; }

        [JsonPropertyName("relative_humidity")]
        public double RelativeHumidity { get; set; }

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }
        
        [JsonPropertyName("precipitation_amount")]
        public double PrecipitationAmount { get; set; }
    }
}