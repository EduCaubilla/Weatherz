using System.Threading.Tasks;

namespace Weatherz.Services
{
    public class PermissionService
    {
        public PermissionService()
        {
        }

        public async Task<bool> CheckAndRequestLocationPermissionAsync()
        {
            // Check and request location permission
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            Console.WriteLine($"Location permission status: {status}");
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            return status == PermissionStatus.Granted;
        }
    }
}