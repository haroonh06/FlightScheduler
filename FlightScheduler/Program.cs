using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightScheduler.Core;
using FlightScheduler.BL;
using Autofac;

namespace FlightScheduler
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> destinations = new List<string>() { "YYZ", "YYC", "YVR" };
            string hub = "YUL";

            #region INJECT DEPENDENCIES

            var builder = new ContainerBuilder();

            // Register individual components
            builder.Register(c => new FlightSchedule(destinations, 2, hub))
                   .As<IFlightSchedule>();
            builder.Register(c => new OrderInventory())
                   .As<IOrderInventory>();
            builder.Register(c => new OrderRepository())
                   .As<IOrderRepository>();

            var container = builder.Build();

            #endregion



            var ObjectScope = container.BeginLifetimeScope();

            var flightSchedule = ObjectScope.Resolve<IFlightSchedule>();
            
            List<Flight> flights = flightSchedule.BuildFlightSchedule();

            Console.WriteLine("================ Flight Schedule for Transport.ly: ==========================\n");

            //First User story , print planned schedule 
            flights.ForEach(m => Console.WriteLine(string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}", m.FlightNumber, m.Origin, m.Destination, m.DayOfFlight)));

            Console.WriteLine("\nPress any key to continue");

            Console.ReadKey();            

            Console.WriteLine("================ Load diplay for each Flight for Transport.ly: ==========================\n");

            var OrderInventory = ObjectScope.Resolve<IOrderInventory>();
            var OrderRepository = ObjectScope.Resolve<IOrderRepository>();

            var Orders = OrderRepository.GetOrders();
            var scheduledFlights = flights;

            //Build orders & flights Map
            var response= OrderInventory.BookFlightInventory(scheduledFlights,Orders);

            if(response!=null)
            {
                List<Flight> Oflights= response.Keys.First();
                List<Order> orders = response.Values.First();

                if (Oflights != null && Oflights.Count>0)
                {
                    foreach(Flight flight in Oflights)
                    {
                        if(flight.Orders!=null && flight.Orders.Count>0)
                        {
                            flight.Orders.ForEach(m => Console.WriteLine("order: order-"+m.OrderNo.ToString("D3") + ",flightNumber: " + flight.FlightNumber + ", departure: "+ flight.Origin + ", arrival: "+ flight.Destination + ", day: "+ flight.DayOfFlight));
                        }
                    }                        
                }

                if (orders != null && orders.Count > 0)
                {
                    orders.ForEach(m => Console.WriteLine("order: order-" + m.OrderNo.ToString("D3") + ",flightNumber: not scheduled"));
                }
            }
            else
            {
                Console.WriteLine("==============Error:=======================================");
            }

            Console.ReadKey();
        }
    }
}
