using FlightScheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduler.BL
{
    public interface IFlightSchedule
    {
        List<Flight> BuildFlightSchedule();        
    }
}
