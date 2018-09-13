using AirTr.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirTr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowController : Window
    {

        //private  Map _map;
        private AirController _provider;
        //private ObservableCollection<Aircraft> _obs = new ObservableCollection<Aircraft>();

        public WindowController()
        {
            InitializeComponent();
            //_map = mainMap;
            //_map.Focus();
            _provider = new AirController();

            //_obs.Add(new Aircraft{ Id = 12, Identifier = "asd", Model = "Small" });
            //_obs.Add(new Aircraft{ Id = 14, Identifier = "4", Model = "Large" });
            //_obs.Add(new Aircraft{ Id = 13, Identifier = "Hello", Model = "Smal2l" });

            _provider.GetAircrafts(55.8581f, 9.8476f, 50);
            details.ItemsSource = _provider.AircraftList;

            

        }

        private void AddImageToMap()
        {
            MapLayer imageLayer = new MapLayer();


            Image image = new Image();
            image.Height = 150;
            //Define the URI location of the image
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\users\shadow\desktop\plane.png");
            // To save significant application memory, set the DecodePixelWidth or  
            // DecodePixelHeight of the BitmapImage value of the image source to the desired 
            // height or width of the rendered image. If you don't do this, the application will 
            // cache the image as though it were rendered as its normal size rather then just 
            // the size that is displayed.
            // Note: In order to preserve aspect ratio, set DecodePixelWidth
            // or DecodePixelHeight but not both.
            //Define the image display properties
            myBitmapImage.DecodePixelHeight = 150;
            myBitmapImage.EndInit();
            image.Source = myBitmapImage;
            image.Opacity = 0.6;
            image.Stretch = System.Windows.Media.Stretch.None;

            //The map location to place the image at
            Location location = new Location() { Latitude = 55.8581, Longitude = 9.8476 };
            //Center the image around the location specified
            PositionOrigin position = PositionOrigin.Center;

            //Add the image to the defined map layer
            imageLayer.AddChild(image, location, position);
            //Add the image layer to the map
            //_map.Children.Add(imageLayer);
        }

        private void ButtClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(_provider.AircraftList.Count);
        }
    }
}
