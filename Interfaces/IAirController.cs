using AirTr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTr.Interfaces
{
    interface IAirController
    {
        void GetAircrafts(MapLocation location, int distance);
        void GetAircrafts(float lat, float lon, int distance);
    }
}
