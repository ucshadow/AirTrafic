using AirTr.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace AirTr.Classes
{
    public static class MapController
    {

        public static Map Map;

        private static Random _random = new Random();
        private static Dictionary<int, SolidColorBrush> _colors = new Dictionary<int, SolidColorBrush>();

        public static void AddImageToMap(float latitude, float longitude)
        {
            MapLayer imageLayer = new MapLayer();


            var image = new Image();
            image.Height = 16;
            //Define the URI location of the image
            var myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\users\shadow\desktop\plane.png");
            myBitmapImage.DecodePixelHeight = 16;
            myBitmapImage.EndInit();
            image.Source = myBitmapImage;
            //image.Opacity = 0.6;
            image.Stretch = Stretch.None;

            //The map location to place the image at
            var location = new Location() { Latitude = latitude, Longitude = longitude };
            //Center the image around the location specified
            var position = PositionOrigin.Center;

            //Add the image to the defined map layer
            imageLayer.AddChild(image, location, position);
            //Add the image layer to the map
            Map.Children.Add(imageLayer);
        }

        public static void DrawOriginLine(Aircraft aircraft)
        {
            var color = new SolidColorBrush(Colors.Aqua);
            if (_colors.ContainsKey(aircraft.Id))
            {
                color = _colors[aircraft.Id];
            }
            else
            {
                color = new SolidColorBrush(Color.FromRgb((byte)_random.Next(255), (byte)_random.Next(255), (byte)_random.Next(255)));
                _colors.Add(aircraft.Id, color);
            }

            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = color;
            polyline.StrokeThickness = 3;
            polyline.Opacity = 0.5;
            polyline.Locations = new LocationCollection() {
            new Location(aircraft.OriginLat, aircraft.OriginLon),
            new Location(aircraft.Lat, aircraft.Lon)};

            Map.Children.Add(polyline);
        }

        public static void DrawDestinationLine(Aircraft aircraft)
        {

            var color = new SolidColorBrush(Colors.Aqua);
            if (_colors.ContainsKey(aircraft.Id))
            {
                color = _colors[aircraft.Id];
            }
            else
            {
                color = new SolidColorBrush(Color.FromRgb((byte)_random.Next(255), (byte)_random.Next(255), (byte)_random.Next(255)));
                _colors.Add(aircraft.Id, color);
            }

            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = color;
            polyline.StrokeThickness = 1;
            polyline.Opacity = 0.5;
            polyline.Locations = new LocationCollection() {
            new Location(aircraft.DestinationLat, aircraft.DestinationLon),
            new Location(aircraft.Lat, aircraft.Lon)};

            Map.Children.Add(polyline);
        }

    }
}
