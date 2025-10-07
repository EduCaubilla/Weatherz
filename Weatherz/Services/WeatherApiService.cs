using System;
using System.Threading.Tasks;
using Weatherz.Models;

namespace Weatherz.Services
{
    internal class WeatherApiService
    {
        private readonly HttpClient _httpClient;

        public WeatherApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Utils.Constants.BASE_URI);
        }

        // Example method to get weather data
        public async Task<WeatherApiResponse> GetWeatherInformation(double latitude, double longitude)
        {
            if(Utils.Tools.IsConnected)
            {
                var url = $"{Utils.Constants.BASE_URI}current?access_key={Utils.Constants.API_KEY}&query={latitude},{longitude}";
                var response = await _httpClient.GetStringAsync(url);
                var weatherResponse = System.Text.Json.JsonSerializer.Deserialize<WeatherApiResponse>(response);
                if (weatherResponse == null)
                {
                    throw new InvalidOperationException("Failed to deserialize weather data.");
                }
                return weatherResponse;
            }
            else
            {
                throw new InvalidOperationException("No internet connection.");
            }
        }
    }
}