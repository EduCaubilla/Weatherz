using System;

namespace Weatherz.Utils
{
    public class Tools
    {
        public static bool IsConnected
        {
            get
            {
                var accessType = Connectivity.Current.NetworkAccess;
                return accessType == NetworkAccess.Internet;
            }
        }

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        public Tools(){}

        public static async void DisplayMessage(string title, string content, string buttonAccept)
        {
            if (Application.Current == null) return;
            
            var page = Application.Current.Windows.FirstOrDefault()?.Page;
            if (page != null)
            {
                await page.DisplayAlert(title, content, buttonAccept);
            }
        }
    }
}