using FlightScheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduler.BL
{
    public class OrderInventory: IOrderInventory
    {
        #region Variables & properties
        
        private const int FlightSeatCapacity = 20;
        private Dictionary<string, int> orderMap = new Dictionary<string, int>();

        #endregion

       

        #region Methods

        public OrderInventory()
        {
            
        }

        public Dictionary<List<Flight>,List<Order>> BookFlightInventory(List<Flight> scheduledFlights, List<Order> Orders)
        {
            Dictionary<List<Flight>, List<Order>> keyValuePair = new Dictionary<List<Flight>, List<Order>>();

            if (scheduledFlights.Count>0 && Orders.Count>0)
            {
                foreach(Flight flight in scheduledFlights)
                {
                    int maxOrderNO = 0;

                    //if(orderMap.ContainsKey(flight.Destination))
                    //{
                    //    maxOrderNO = orderMap[flight.Destination];
                    //}

                    var fOrders= Orders.Where(k => k.OrderDestination.Equals(flight.Destination) && k.OrderNo>maxOrderNO).Take(20);

                    if(fOrders!=null && fOrders.Count()>0)
                    {
                        flight.Orders = fOrders.ToList();
                        
                        //if (!orderMap.ContainsKey(flight.Destination))
                        //{
                        //    orderMap.Add(flight.Destination, fOrders.Max(m => m.OrderNo));
                        //}
                        //else
                        //{
                        //    orderMap[flight.Destination]= fOrders.Max(m => m.OrderNo);
                        //}

                        Orders.RemoveAll(m => fOrders.Any(k => k.OrderNo == m.OrderNo));
                    }
                }

                keyValuePair.Add(scheduledFlights, Orders);
            }

            return keyValuePair;
        }

        #endregion
    }
}
