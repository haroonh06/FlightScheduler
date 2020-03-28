using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduler.Core
{
    public class Flight
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int DayOfFlight { get; set; }

        public int FlightNumber { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderNo { get; set; }
        public string OrderDestination { get; set; }
    }

   
}
