using FlightScheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduler.BL
{
    public interface IOrderInventory
    {
        Dictionary<List<Flight>, List<Order>> BookFlightInventory(List<Flight> scheduledFlights, List<Order> Orders);
    }
}
