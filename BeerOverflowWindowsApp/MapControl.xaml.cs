﻿using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Maps.MapControl.WPF.Design;
using System.Windows.Input;
using System.Configuration;
using System;

namespace BeerOverflowWindowsApp
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        double latitude = CurrentLocation.currentLocation.Latitude;
        double longitude = CurrentLocation.currentLocation.Longitude;

        public MapControl()
        {
            InitializeComponent();
            Pushpin pin = new Pushpin();
            pin.Location = new Location(latitude, longitude);
            Location center = new Location(latitude, longitude);
            Map.Children.Add(pin);
            double zoom = 13.000;
            Map.SetView(center, zoom);
        }

        private void ShowCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            Location center = new Location(latitude, longitude);
            double zoom = 16.000;
            Map.SetView(center, zoom);
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = Map.ViewportPointToLocation(mousePosition);
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            Map.Children.Add(pin);
        }
    }
}
