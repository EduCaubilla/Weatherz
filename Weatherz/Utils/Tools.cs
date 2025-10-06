using System;

namespace Weatherz.Utils
{
    public class Tools
    {
        public bool IsConnected;

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        public Tools()
        {
            IsConnected = accessType == NetworkAccess.Internet;
        }

        public async void DisplayMessage(string title, string content, string buttonAccept)
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