using AirTr.Classes;
using AirTr.Models;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
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

        private  Map _map;
        //private AirController _provider;
        //private ObservableCollection<Aircraft> _obs = new ObservableCollection<Aircraft>();

        public WindowController()
        {
            InitializeComponent();
            _map = mainMap;
            _map.CredentialsProvider = new ApplicationIdCredentialsProvider("");
            _map.Focus();
            var loop = new MainLoop() { Dispatcher = Dispatcher };
            MapController.Map = _map;
            MapController.CreateImages();
            MapController.AircraftList = loop.GetAircraftList();
            objectTracker.DataContext = loop.GetAircraftList();
            FileHandler.CreateLocationFile();

            loop.StartSequence();
            details.ItemsSource = loop.GetAircraftList();

        }

        private void ButtClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("1");
        }
    }
}
