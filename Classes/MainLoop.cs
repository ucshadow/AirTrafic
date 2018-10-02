using AirTr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirTr.Classes
{
    class MainLoop
    {

        public int UpdateInterval { get; set; }
        public int Radius { get; set; }
        public AirController AirController;
        public Dispatcher Dispatcher { get; set; }

        public MainLoop()
        {
            UpdateInterval = 20000;
            AirController = new AirController();
        }

        public MainLoop(int updateInterval)
        {
            UpdateInterval = updateInterval;
            AirController = new AirController();
        }

        public void StartSequence()
        {
            if (Radius == 0) Radius = 100;

            // todo: erase everything here

            AirController.ClearAircrafts();

            AirController.GetAircrafts(55.8581f, 9.8476f, Radius);

            new Thread(Loop).Start();
        }

        public void Loop()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Sleeping");
            Console.WriteLine("----------------------------------------------------------");
            Thread.Sleep(UpdateInterval);

            Dispatcher.BeginInvoke(new Action(() => { StartSequence(); }));
            
        }

        public ObservableCollection<Aircraft> GetAircraftList()
        {
            return AirController.AircraftList;
        }
    }
}
