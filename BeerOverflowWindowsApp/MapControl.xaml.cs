using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Input;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl
    {
        private readonly string _defaultStartingZoom = ConfigurationManager.AppSettings["map_startingZoomLevel"];
        private readonly string _defaultCurrentLocationZoom = ConfigurationManager.AppSettings["map_currentLocationZoomLevel"];
        private readonly double _latitude = CurrentLocation.currentLocation.Latitude;
        private readonly double _longitude = CurrentLocation.currentLocation.Longitude;
        //private MapLayer RouteLayer;

        public MapControl(BarDataModel barList)
        {
            if (CurrentLocation.currentLocation == null)
            {
                throw new ArgumentNullException();
            }
            if (barList == null)
            {
                throw new ArgumentNullException(nameof(barList));
            }
            InitializeComponent();
            var center = new Location(_latitude, _longitude);
            var pin = new Pushpin {Location = center};
            pin.Name = "wtf";
            pin.Background = new SolidColorBrush(Colors.Blue);
            Map.Children.Add(pin);
            var zoom = Convert.ToDouble(_defaultStartingZoom, CultureInfo.InvariantCulture);
            Map.SetView(center, zoom);

            foreach (var bar in barList)
            {
                var barLocation = new Location(bar.Latitude, bar.Longitude);
                var barPin = new Pushpin
                {
                    Location = barLocation,
                    Background = new SolidColorBrush(Colors.OliveDrab)
                };
                Map.Children.Add(barPin);
            }
        }
        
        private Pushpin _editPin = null;
        protected Pushpin editPin
        {
            get
            {
                return _editPin;
            }
            set
            {
                _editPin = value;
            }
        }

        void pin_MouseEnter(object sender, MouseEventArgs e)
        {
            Pushpin pin = (Pushpin)sender;
            // Use the pushpin's Name property for the title and the Tag                // property for the description.
            InfoboxTitle.Text = pin.Name;
            InfoboxDescription.Text = pin.Tag.ToString();
            // Show the infobox.
            Infobox.Visibility = Visibility.Visible;
            MapLayer.SetPosition(Infobox, pin.Location);
            MapLayer.SetPositionOrigin(Infobox, PositionOrigin.BottomCenter);
            MapLayer.SetPositionOffset(Infobox, new Point(0, -50));
        }

        void pin_MouseLeave(object sender, MouseEventArgs e)
        {
            // The user has moved the mouse away from the pushpin.            // If we're not in the edit state, hide the infobox.
            Infobox.Visibility = Visibility.Collapsed;
            
        }

        void pin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // The user clicked a pushpin. If we're not in the edit state,            // show the textbox to edit this pushpin's text.
                // Store a copy of the pushpin we're editing.
            editPin = (Pushpin)sender;
            // Show editable text box and close button.
            InfoboxDescription.Visibility = Visibility.Collapsed;
            InfoboxDescriptionEdit.Visibility = Visibility.Visible;
            InfoboxDescriptionEdit.Text = editPin.Tag.ToString();
            btnClose.Visibility = Visibility.Visible;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // The user clicked the 'X' close button from editing a pushpin's text.
            // Hide editable text box and close button.
            InfoboxDescription.Visibility = Visibility.Visible;
            InfoboxDescriptionEdit.Visibility = Visibility.Collapsed;
            btnClose.Visibility = Visibility.Collapsed;
            Infobox.Visibility = Visibility.Collapsed;
            // Save text changes in the pushpin.
            editPin.Tag = InfoboxDescriptionEdit.Text;
            // Clear out the edit pushpin in memory and reset state.
            editPin = null;
        }

        /*
        private void MouseOnPushpin_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var mousePosition = e.GetPosition(this);
            var pinLocation = Map.ViewportPointToLocation(mousePosition);
            var pin = new Pushpin { Location = pinLocation };
            Console.WriteLine(pinLocation);
            Map.Children.Add(pin);
        }
        */

        private void ShowCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            var center = new Location(_latitude, _longitude);
            var zoom = Convert.ToDouble(_defaultCurrentLocationZoom, CultureInfo.InvariantCulture);
            Map.SetView(center, zoom);
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
