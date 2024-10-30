using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Core.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public double Weight { get; set; }

        public string CityDistrict { get; set; }

        public DateTime DeliveryTime { get; set; }
    }
}
