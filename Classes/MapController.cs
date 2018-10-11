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
using System.Collections.ObjectModel;
using static AirTr.Util.Helpers;

namespace AirTr.Classes
{
    public static class MapController
    {

        public static Map Map;

        private static Random _random = new Random();
        private static Dictionary<int, SolidColorBrush> _colors = new Dictionary<int, SolidColorBrush>();

        private static BitmapImage _black;
        private static BitmapImage _red;

        private static Image _selectedImage;
        private static Aircraft _selectedAircraft;

        private static MapLayer _imageLayer = new MapLayer();
        private static MapLayer _lineLayer = new MapLayer();
        private static MapLayer _historyLayer = new MapLayer();

        public static ObservableCollection<Aircraft> AircraftList;

        public static void AddAircraftToMap(Aircraft aircraft)
        {
            var a = AircraftExists(aircraft);
            if (a != null)
            {
                UpdateAircraftPosition(a, aircraft);
                return;
            }
            AddImageToMap(aircraft);
            
        }

        public static void AddHistoryLine()
        {

        }

        private static void AddImageToMap(Aircraft aircraft)
        {
            //Print($"Adding to map {aircraft.Id}");
            var image = new Image
            {
                Height = 16,
                Source = _black,
                Stretch = Stretch.None,
                DataContext = aircraft
            };

            ToolTipService.SetToolTip(image, new ToolTip() { Content = aircraft.Manufacturer });
            image.MouseDown += Image_PreviewMouseDown;
            var location = new Location() { Latitude = aircraft.Lat, Longitude = aircraft.Lon };
            var position = PositionOrigin.Center;
            _imageLayer.AddChild(image, location, position);
        }

        private static void UpdateAircraftPosition(Aircraft oldAircraft, Aircraft newAircraft)
        {
            oldAircraft.Lat = newAircraft.Lat;
            oldAircraft.Lon = newAircraft.Lon;
            
            foreach(var o in _imageLayer.Children)
            {
                var asUI = (System.Windows.UIElement)o;
                var asImage = (Image)asUI;
                var aircraft = (Aircraft)asImage.DataContext;

                if(aircraft.Id == oldAircraft.Id)
                {
                    _imageLayer.Children.Remove(asUI);
                    AddImageToMap(newAircraft);
                    return;
                }
            }
            AddImageToMap(newAircraft);
        }

        private static Aircraft AircraftExists(Aircraft aircraft)
        {
            foreach(var a in AircraftList)
            {
                if(a.Id == aircraft.Id)
                {
                    //Print($"Aircraft exists {a.Id}");
                    return a;
                }
            }
            return null;
        }

        private static void Image_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(_selectedImage != null)
            {
                _selectedImage.Source = _black;
            }

            if(_selectedAircraft != null)
            {
                Console.WriteLine(_selectedAircraft.Background);
                _selectedAircraft.Background = "#FFD2F0D8";
            }

            var image = (Image)sender;
            image.Source = _red;
            _selectedImage = image;

            // get a refrence to the actual aircraft
            var aircraft = AircraftList.Where(x => x.Id == ((Aircraft)image.DataContext).Id).First();
            _selectedAircraft = aircraft;
            aircraft.Background = "#f485d7";

            AircraftList.Move(AircraftList.IndexOf(aircraft), 0);
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

            Map.Children.Add(_imageLayer);
            Map.Children.Add(_lineLayer);

        }

        public static void DrawOriginLine(Aircraft aircraft)
        {

            //Print($"Drawing new Origin line lat {aircraft.OriginLat} lon {aircraft.OriginLon}");

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

            CreateLine(aircraft, 3, color, 0.7f);
            
        }

        public static void DrawDestinationLine(Aircraft aircraft)
        {

            //Print($"Drawing new Destination line lat {aircraft.DestinationLat} lon {aircraft.DestinationLon}");

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

            CreateLine(aircraft, 2, color, 0.7f);
        }

        private static void CreateLine(Aircraft aircraft, int thickness, SolidColorBrush color, float opacity)
        {
            MapPolyline polyline = new MapPolyline
            {
                Stroke = color,
                StrokeThickness = thickness,
                Opacity = opacity,
                Locations = new LocationCollection() {
            new Location(aircraft.DestinationLat, aircraft.DestinationLon),
            new Location(aircraft.Lat, aircraft.Lon)}
            };

            _lineLayer.Children.Add(polyline);
        }

        // todo: add history path.
        internal static void ClearAllDrawings()
        {
            //_imageLayer.Children.Clear();
            _lineLayer.Children.Clear();
            //AircraftList.Clear();
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
