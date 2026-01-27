using System.Net.Http.Json;
using System.Text.Json.Serialization;
using FloodApp.Models;

namespace FloodApp.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResult?> GetCurrentWeatherAsync(double lat, double lng)
    {
        try
        {
            // API: Open-Meteo (Free for non-commercial use, no key required)
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&current=temperature_2m,rain,wind_speed_10m";
            
            var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);
            
            if (response?.Current == null) return null;

            return new WeatherResult
            {
                Temperature = response.Current.Temperature2m,
                Rain = response.Current.Rain,
                WindSpeed = response.Current.WindSpeed10m,
                UnitTemp = response.CurrentUnits.Temperature2m,
                UnitRain = response.CurrentUnits.Rain,
                UnitWind = response.CurrentUnits.WindSpeed10m
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching weather: {ex.Message}");
            return null;
        }
    }

    // Internal classes for Open-Meteo JSON structure
    private class OpenMeteoResponse
    {
        [JsonPropertyName("current_units")]
        public CurrentUnits CurrentUnits { get; set; } = new();

        [JsonPropertyName("current")]
        public CurrentData Current { get; set; } = new();
    }

    private class CurrentUnits
    {
        [JsonPropertyName("temperature_2m")]
        public string Temperature2m { get; set; } = "°C";

        [JsonPropertyName("rain")]
        public string Rain { get; set; } = "mm";

        [JsonPropertyName("wind_speed_10m")]
        public string WindSpeed10m { get; set; } = "km/h";
    }

    private class CurrentData
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature2m { get; set; }

        [JsonPropertyName("rain")]
        public double Rain { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed10m { get; set; }
    }
}

public class WeatherResult
{
    public double Temperature { get; set; }
    public double Rain { get; set; }
    public double WindSpeed { get; set; }
    public string UnitTemp { get; set; } = "°C";
    public string UnitRain { get; set; } = "mm";
    public string UnitWind { get; set; } = "km/h";
}
