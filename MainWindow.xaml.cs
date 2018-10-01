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
        private AirController _provider;
        //private ObservableCollection<Aircraft> _obs = new ObservableCollection<Aircraft>();

        public WindowController()
        {
            InitializeComponent();
            _map = mainMap;
            _map.CredentialsProvider = new ApplicationIdCredentialsProvider("");
            _map.Focus();
            MapController.Map = _map;
            MapController.CreateImages();
            _provider = new AirController();

            FileHandler.CreateLocationFile();

            //_obs.Add(new Aircraft{ Id = 12, Identifier = "asd", Model = "Small" });
            //_obs.Add(new Aircraft{ Id = 14, Identifier = "4", Model = "Large" });
            //_obs.Add(new Aircraft{ Id = 13, Identifier = "Hello", Model = "Smal2l" });

            _provider.GetAircrafts(55.8581f, 9.8476f, 120);
            details.ItemsSource = _provider.AircraftList;

            

        }

        private void ButtClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(_provider.AircraftList.Count);
        }
    }
}
