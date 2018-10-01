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
using System.IO;

namespace AirTr.Classes
{
    public static class MapController
    {

        public static Map Map;

        private static Random _random = new Random();
        private static Dictionary<int, SolidColorBrush> _colors = new Dictionary<int, SolidColorBrush>();

        private static BitmapImage _black;
        private static BitmapImage _red;

        private static Image _selected;

        public static void AddImageToMap(Aircraft aircraft)
        {
            MapLayer imageLayer = new MapLayer();


            var image = new Image
            {
                Height = 16,
                Source = _black,
                Stretch = Stretch.None,
                DataContext = aircraft
            };

            ToolTipService.SetToolTip(image, new ToolTip() { Content = aircraft.Manufacturer });

            image.MouseDown += Image_PreviewMouseDown;

            //The map location to place the image at
            var location = new Location() { Latitude = aircraft.Lat, Longitude = aircraft.Lon };
            //Center the image around the location specified
            var position = PositionOrigin.Center;

            //Add the image to the defined map layer
            imageLayer.AddChild(image, location, position);
            //Add the image layer to the map
            Map.Children.Add(imageLayer);
        }

        private static void Image_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(_selected != null)
            {
                _selected.Source = _black;
            }
            var image = (Image)sender;
            image.Source = _red;
            _selected = image;
            var aircraft = (Aircraft)image.DataContext;
            Console.WriteLine(aircraft.Call);
        }

        public static void CreateImages()
        {

            _black = new BitmapImage();
            _black.BeginInit();
            _black.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\images\black.png");
            _black.DecodePixelHeight = 16;
            _black.EndInit();

            _red = new BitmapImage();
            _red.BeginInit();
            _red.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\images\red.png");
            _red.DecodePixelHeight = 16;
            _red.EndInit();

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

        [Obsolete("AddImageToMap(float, float) is deprecated, use AddImageToMap(Aircraft) instead")]
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

            image.MouseDown += Image_PreviewMouseDown;

            //The map location to place the image at
            var location = new Location() { Latitude = latitude, Longitude = longitude };
            //Center the image around the location specified
            var position = PositionOrigin.Center;

            //Add the image to the defined map layer
            imageLayer.AddChild(image, location, position);
            //Add the image layer to the map
            Map.Children.Add(imageLayer);
        }

    }
}
