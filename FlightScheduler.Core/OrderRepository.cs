using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FlightScheduler.Core
{
    public class OrderRepository:IOrderRepository
    {
        public  List<Order> GetOrders()
        {
            List<Order> lstOrders = new List<Order>();
            string jsonString=string.Empty;

            if (File.Exists("Data.json"))
            {
                jsonString = File.ReadAllText("Data.json");
            }

            if (!string.IsNullOrEmpty(jsonString))
            {
                var jObj = JObject.Parse(jsonString);

                lstOrders = (from obj in jObj.Values()
                         select new Order { OrderNo = Convert.ToInt16(((JToken)obj).Path.Trim().Substring(6)), OrderDestination = ((JToken)obj).Value<string>("destination") }).ToList();
                
                         }

            return lstOrders;
        }
    }
}
