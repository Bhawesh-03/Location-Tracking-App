using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Maps;
using System.Diagnostics;

namespace LocationTrackingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            // Set an initial map location
            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Location(37.7749, -122.4194), // Example: San Francisco coordinates
                Distance.FromKilometers(10)));
        }

        private async void TrackLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Get the user's current location
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    // Add a pin to the map
                    var pin = new Pin
                    {
                        Label = "Your Location",
                        Address = "Current Location",
                        Location = new Location(location.Latitude, location.Longitude)
                    };
                    MapView.Pins.Add(pin);

                    // Center the map on the new location
                    MapView.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Location(location.Latitude, location.Longitude),
                        Distance.FromKilometers(1)));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving location: {ex.Message}");
            }
        }
    }
}
