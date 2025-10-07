using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Weatherz.Models
{
    // Root object for the API response
    public partial class WeatherApiResponse
    {
        [JsonPropertyName("request")]
        public RequestInfo? Request { get; set; }

        [JsonPropertyName("location")]
        public LocationInfo? Location { get; set; }

        [JsonPropertyName("current")]
        public CurrentInfo? Current { get; set; }
    }

    public partial class WeatherApiResponse
    {
        public static WeatherInfoModel mapToModel(WeatherApiResponse apiResponse)
        {
            if (apiResponse.Current == null || apiResponse.Location == null)
                throw new ArgumentException("API response is missing required data.");

            var weatherInfo = new WeatherInfoModel
            {
                WeatherIcon = apiResponse.Current.WeatherIcons?.FirstOrDefault() ?? "unknown.png",
                TemperatureCelsius = apiResponse.Current.Temperature ?? 0.0,
                LocationName = $"{apiResponse.Location.Name}, {apiResponse.Location.Country}",
                WeatherDescription = apiResponse.Current.WeatherDescriptions?.FirstOrDefault() ?? "",
                Humidity = apiResponse.Current.Humidity ?? 0,
                WindSpeedKph = apiResponse.Current.WindSpeed ?? 0.0,
                WindDirection = apiResponse.Current.WindDirection ?? "Unknown",
                UvIndex = apiResponse.Current.UvIndex ?? 0,
                FeelsLikeCelsius = apiResponse.Current.FeelsLike ?? 0.0,
                Localtime = apiResponse.Location.Localtime ?? "",
                Sunrise = apiResponse.Current.Astro?.Sunrise ?? "",
                Sunset = apiResponse.Current.Astro?.Sunset ?? "",
                IsDayTime = apiResponse.Current.IsDay == "yes"
            };

            return weatherInfo;
        }
    }

    public class RequestInfo
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("query")]
        public string? Query { get; set; }
        [JsonPropertyName("language")]
        public string? Language { get; set; }
        [JsonPropertyName("unit")]
        public string? Unit { get; set; }
    }

    public class LocationInfo
    {
        [JsonPropertyName("name")] 
        public string? Name { get; set; }

        [JsonPropertyName("country")] 
        public string? Country { get; set; }

        [JsonPropertyName("region")] 
        public string? Region { get; set; }

        [JsonPropertyName("lat")] 
        public string? Latitude { get; set; }

        [JsonPropertyName("lon")] 
        public string? Longitude { get; set; }

        [JsonPropertyName("timezone_id")] 
        public string? TimezoneId { get; set; }

        [JsonPropertyName("localtime")] 
        public string? Localtime { get; set; }

        [JsonPropertyName("localtime_epoch")] 
        public long? LocaltimeEpoch { get; set; }

        [JsonPropertyName("utc_offset")] 
        public string? UtcOffset { get; set; }
    }

    public class CurrentInfo
    {
        [JsonPropertyName("observation_time")] 
        public string? ObservationTime { get; set; }

        [JsonPropertyName("temperature")] 
        public double? Temperature { get; set; }

        [JsonPropertyName("weather_code")] 
        public int? WeatherCode { get; set; }

        [JsonPropertyName("weather_icons")] 
        public List<string>? WeatherIcons { get; set; }

        [JsonPropertyName("weather_descriptions")] 
        public List<string>? WeatherDescriptions { get; set; }

        [JsonPropertyName("astro")] 
        public AstroInfo? Astro { get; set; }

        [JsonPropertyName("air_quality")] 
        public AirQualityInfo? AirQuality { get; set; }

        [JsonPropertyName("wind_speed")] 
        public double? WindSpeed { get; set; }

        [JsonPropertyName("wind_degree")] 
        public int? WindDegree { get; set; }

        [JsonPropertyName("wind_dir")] 
        public string? WindDirection { get; set; }

        [JsonPropertyName("pressure")] 
        public double? Pressure { get; set; }

        [JsonPropertyName("precip")] 
        public double? Precip { get; set; }

        [JsonPropertyName("humidity")] 
        public int? Humidity { get; set; }

        [JsonPropertyName("cloudcover")] 
        public int? Cloudcover { get; set; }

        [JsonPropertyName("feelslike")] 
        public double? FeelsLike { get; set; }

        [JsonPropertyName("uv_index")] 
        public int? UvIndex { get; set; }

        [JsonPropertyName("visibility")] 
        public double? Visibility { get; set; }

        [JsonPropertyName("is_day")] 
        public string? IsDay { get; set; }
    }

    public class AstroInfo
    {
        [JsonPropertyName("sunrise")]
        public string? Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string? Sunset { get; set; }

        [JsonPropertyName("moonrise")]
        public string? Moonrise { get; set; }

        [JsonPropertyName("moonset")]
        public string? Moonset { get; set; }

        [JsonPropertyName("moon_phase")]
        public string? MoonPhase { get; set; }

        [JsonPropertyName("moon_illumination")]
        public int? MoonIllumination { get; set; }
    }

    public class AirQualityInfo
    {
        [JsonPropertyName("co")] 
        public string? CO { get; set; }

        [JsonPropertyName("no2")] 
        public string? NO2 { get; set; }

        [JsonPropertyName("o3")] 
        public string? O3 { get; set; }

        [JsonPropertyName("so2")] 
        public string? SO2 { get; set; }

        [JsonPropertyName("pm2_5")] 
        public string? PM25 { get; set; }

        [JsonPropertyName("pm10")] 
        public string? PM10 { get; set; }

        [JsonPropertyName("us-epa-index")] 
        public string? UsEpaIndex { get; set; }

        [JsonPropertyName("gb-defra-index")] 
        public string? GbDefraIndex { get; set; }
    }
}
