using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiBookingV2.Vehicle;

namespace TaxiBookingV2.Models
{
    public class Raid
    {
        public DateTime PickUpTime { get; set; }
        public DateTime DropTime { get; set; }
        public LPOINT PickupPoint { get; set; }
        public LPOINT DropPoint { get; set; }
        
    }
}
