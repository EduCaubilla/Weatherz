using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Weatherz.Models;
using Weatherz.Services;

namespace Weatherz.ViewModels
{
    public class MainInfoScreenViewModel : INotifyPropertyChanged
    {
        private readonly WeatherApiService _weatherApiService;
        private readonly PermissionService _permissionService;
        private WeatherInfoModel? _weatherInfo;
        private bool _isBusy;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Raise([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public WeatherInfoModel? WeatherInfo
        {
            get => _weatherInfo;
            set
            {
                _weatherInfo = value;
                Raise();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                Raise();
            }
        }
        public bool IsDaytime => WeatherInfo?.IsDayTime ?? true;

        public MainInfoScreenViewModel()
        {
            _weatherApiService = new WeatherApiService();
            _permissionService = new PermissionService();
        }

        public MainInfoScreenViewModel(bool initTestData)
        {
            _weatherApiService = new WeatherApiService();
            _permissionService = new PermissionService();

            if (!initTestData) return;

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
                Localtime = DateTime.Now.ToString("HH:mm")
            };
        }

        public async Task OnAppearingAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                await setGeolocationProcess();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task setGeolocationProcess()
        {
            if (Utils.Tools.IsConnected)
            {
                var permissionStatus = await _permissionService.CheckAndRequestLocationPermissionAsync();
                if (!permissionStatus)
                {
                    Utils.Tools.DisplayMessage("Location allowance error", "This app needs permission to access location to show weather information.\nPlease switch the permission on.", "Ok");
                    return;
                }

                await getDeviceLocationAsync();
            }
            else
            {
                Utils.Tools.DisplayMessage("No Internet", "Please check your internet connection and try again.", "Ok");
            }
        }

        private async Task getDeviceLocationAsync()
        {
            try
            {
                var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(5)
                });

                Console.WriteLine($"Location: {location?.Latitude}, {location?.Longitude}");

                if (location != null)
                {
                    await FetchWeatherData(location.Latitude, location.Longitude);
                }
            }
            catch (Exception ex)
            {
                Utils.Tools.DisplayMessage("Location Error", "Unable to get device location. Please ensure location services are enabled.", "Ok");
                Console.WriteLine($"Error obtaining location: {ex.Message}");
            }
        }

        private async Task FetchWeatherData(double latitude, double longitude)
        {
            try
            {
                var apiResponse = await _weatherApiService.GetWeatherInformation(latitude, longitude);
                WeatherInfo = WeatherApiResponse.mapToModel(apiResponse);
                Console.WriteLine($"Weather Description: {WeatherInfo.WeatherDescription}");
            }
            catch (Exception ex)
            {
                Utils.Tools.DisplayMessage("Error fetching data.", "Unable to get weather information. Please try again later.", "Ok");
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
            }
        }
    }
}
