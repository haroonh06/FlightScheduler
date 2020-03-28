using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightScheduler.Core;

namespace FlightScheduler.BL
{
    public class FlightSchedule: IFlightSchedule
    {
        #region Member variables

        private string hub="YUL";
        private List<string> destinations = null;
        private int noflightDays;
        
        public List<Flight> ScheduledFlights { get; private set; }
        #endregion

        public FlightSchedule(List<string> Destinations,int MaxFlightdays,string Hub)
        {
            hub = Hub;
            destinations = Destinations;
            noflightDays = MaxFlightdays;            
        }

        public List<Flight> BuildFlightSchedule()
        {
            List<Flight> lstFlights = new List<Flight>();

            if(destinations.Count>0 && !string.IsNullOrEmpty(hub) && noflightDays>0)
            {
                //In this scenario we are only including roundtrip  scenario and assuming frequency is one  as per user story
                int fDay = 1, counter = 1;

                while (fDay <= noflightDays)
                {
                    //One way
                    foreach (string city in destinations)
                    {
                        lstFlights.Add(new Flight()
                        {
                            DayOfFlight = fDay,Destination= city,
                            FlightNumber= counter,Origin=hub
                        });

                        counter++;
                    }

                    //Return
                    foreach (string city in destinations)
                    {
                        lstFlights.Add(new Flight()
                        {
                            DayOfFlight = fDay,
                            Destination = hub,
                            FlightNumber = counter,
                            Origin = city
                        });

                        counter++;
                    }

                    fDay++;
                }
            }

            ScheduledFlights = lstFlights;

            return lstFlights;
        }

    }
}
