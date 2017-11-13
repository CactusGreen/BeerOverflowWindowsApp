using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Input;
using System;
using System.Configuration;
using System.Globalization;

namespace BeerOverflowWindowsApp
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl
    {
        private readonly string _defaultStartingZoom = ConfigurationManager.AppSettings["map_startingZoomLevel"];
        private readonly string _defaultCurrentLocationZoom =
            ConfigurationManager.AppSettings["map_currentLocationZoomLevel"];

        public MapControl()
        {
            InitializeComponent();
            if (CurrentLocation.currentLocation != null)
            {
                var latitude = CurrentLocation.currentLocation.Latitude;
                var longitude = CurrentLocation.currentLocation.Longitude;
                var center = new Location(latitude, longitude);
                var pin = new Pushpin { Location = center };
                var zoom = Convert.ToDouble(_defaultStartingZoom, CultureInfo.InvariantCulture);
                Map.Children.Add(pin);
                Map.SetView(center, zoom);
            }
        }

        private void ShowCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            var latitude = CurrentLocation.currentLocation.Latitude;
            var longitude = CurrentLocation.currentLocation.Longitude;
            var center = new Location(latitude, longitude);
            var zoom = Convert.ToDouble(_defaultCurrentLocationZoom, CultureInfo.InvariantCulture);
            Map.SetView(center, zoom);
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var mousePosition = e.GetPosition(this);
            var pinLocation = Map.ViewportPointToLocation(mousePosition);
            var pin = new Pushpin {Location = pinLocation};
            Console.WriteLine(pinLocation);
            Map.Children.Add(pin);
        }

        private void ChangeMapMode_Click(object sender, RoutedEventArgs e)
        {
            switch (Map.Mode.ToString())
            {
                case "Microsoft.Maps.MapControl.WPF.RoadMode":
                    Map.Mode = new AerialMode(true);
                    break;
                case "Microsoft.Maps.MapControl.WPF.AerialMode":
                    Map.Mode = new RoadMode();
                    break;
            }
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            var close = new MapWindow();
            close.MapWindowForm();
        }
    }
}
