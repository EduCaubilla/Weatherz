namespace Weatherz.Utils
{
    public static class Constants
    {
        // Add your constant values here
        public const string BASE_URI = "https://api.weatherstack.com/";
        public const string API_KEY = ApiKey.API_KEY;

        public const string TEST_QUERY = $"http://api.weatherstack.com/current?access_key={ApiKey.API_KEY}&query=41.397974,2.159157";
    }
}