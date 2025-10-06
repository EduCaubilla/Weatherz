using System;

namespace Weatherz.Models
{
    public class WeatherInfoModel
    {
        public string WeatherIcon { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit => TemperatureCelsius * 9 / 5 + 32;
        public string LocationName { get; set; }
        public string WeatherDescription { get; set; }
        public double Humidity { get; set; }
        public double WindSpeedKph { get; set; }
        public double WindSpeedMph => WindSpeedKph * 0.621371;
        public double FeelsLikeCelsius { get; set; }
        public double FeelsLikeFahrenheit => FeelsLikeCelsius * 9 / 5 + 32;
        public int UvIndex { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}