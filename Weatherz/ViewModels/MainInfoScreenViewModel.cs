using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Weatherz.Models;

namespace Weatherz.ViewModels
{
    public class MainInfoScreenViewModel : INotifyPropertyChanged
    {
        private WeatherInfoModel? _weatherInfo;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Raise([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// The underlying domain model. Setting this will update all exposed properties and raise PropertyChanged for dependents.
        /// </summary>
        public WeatherInfoModel? WeatherInfo
        {
            get => _weatherInfo;
            set
            {
                _weatherInfo = value;
                // UpdateFromModel();
                Raise();
            }
        }

        // Mapped / formatted properties for binding in the view
        // public string WeatherIcon => Model?.WeatherIcon ?? "sunny.png";

        // public double TemperatureCelsius => Model?.TemperatureCelsius ?? 0.0;

        // public string TemperatureString => Model is null ? "0\u00b0" : $"{Model.TemperatureCelsius:0}\u00b0";

        // public string City => Model?.LocationName ?? "Unknown";

        // public string HumidityPercent => Model is null ? "0%" : $"{Model.Humidity:0}%";

        // public string WindSpeed => Model is null ? "0 km/h" : $"{Model.WindSpeedKph:0} km/h";

        // public string UvIndex => Model is null ? "0" : Model.UvIndex.ToString();

        // public string Sensation => Model is null ? "0\u00b0" : $"{Model.FeelsLikeCelsius:0}\u00b0";

        /// <summary>
        /// True if the current observation time is between sunrise and sunset (basic local-time based heuristic).
        /// Falls back to true if times are not parsable.
        /// </summary>
        public bool IsDaytime
        {
            get
            {
                if (_weatherInfo == null)
                    return true;

                // Try parse sunrise / sunset strings as TimeSpan or DateTime. The model stores them as strings; assume HH:mm or ISO time.
                try
                {
                    var obs = _weatherInfo.ObservationTime;

                    if (!string.IsNullOrEmpty(_weatherInfo.Sunrise) && !string.IsNullOrEmpty(_weatherInfo.Sunset))
                    {
                        if (DateTime.TryParse(_weatherInfo.Sunrise, out var sunriseDt) && DateTime.TryParse(_weatherInfo.Sunset, out var sunsetDt))
                        {
                            // If sunrise/sunset probably are dates without timezone, compare only time of day in observation's date.
                            var sunrise = new DateTime(obs.Year, obs.Month, obs.Day, sunriseDt.Hour, sunriseDt.Minute, 0);
                            var sunset = new DateTime(obs.Year, obs.Month, obs.Day, sunsetDt.Hour, sunsetDt.Minute, 0);
                            return obs >= sunrise && obs <= sunset;
                        }

                        // fallback to parsing as TimeSpan
                        if (TimeSpan.TryParse(_weatherInfo.Sunrise, out var ss) && TimeSpan.TryParse(_weatherInfo.Sunset, out var se))
                        {
                            var t = obs.TimeOfDay;
                            return t >= ss && t <= se;
                        }
                    }
                }
                catch
                {
                    // ignore parse errors and return a default
                }

                return true;
            }
        }

        /// <summary>
        /// Replace the ViewModel data from the model and raise property changed notifications for all mapped properties.
        /// </summary>
        // private void UpdateFromModel()
        // {
        //     // Raise change notifications for all properties bound in the view
        //     Raise(nameof(WeatherIcon));
        //     Raise(nameof(TemperatureCelsius));
        //     Raise(nameof(TemperatureString));
        //     Raise(nameof(City));
        //     Raise(nameof(HumidityPercent));
        //     Raise(nameof(WindSpeed));
        //     Raise(nameof(UvIndex));
        //     Raise(nameof(Sensation));
        //     Raise(nameof(IsDaytime));
        // }

        public MainInfoScreenViewModel()
        {
            _weatherInfo = new WeatherInfoModel
            {
                WeatherIcon = "sunny.png",
                TemperatureCelsius = 22.0,
                LocationName = "San Francisco",
                WeatherDescription = "Clear",
                Humidity = 64,
                WindSpeedKph = 13,
                FeelsLikeCelsius = 23.0,
                UvIndex = 3,
                Sunrise = DateTime.Now.Date.AddHours(6).ToString("HH:mm"),
                Sunset = DateTime.Now.Date.AddHours(18).ToString("HH:mm"),
                ObservationTime = DateTime.Now
            };
        }

        public MainInfoScreenViewModel(WeatherInfoModel initialModel)
        {
            _weatherInfo = initialModel ?? throw new ArgumentNullException(nameof(initialModel));
        }
    }
}
