using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiBookingV2.Models;
using TaxiBookingV2.Vehicle;

namespace TaxiBookingV2
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create Raid
            Raid raid1 = new Raid();
            raid1.PickupPoint = LPOINT.B;
            raid1.DropPoint = LPOINT.D;
            raid1.PickUpTime = DateTime.Now;

            Raid raid2 = new Raid();
            raid2.PickupPoint = LPOINT.B;
            raid2.DropPoint = LPOINT.D;
            raid2.PickUpTime = DateTime.Now;


            Booking MyCab = new Booking();
            MyCab.BookVehicle(raid1);
            
            MyCab.BookVehicle(raid2);
            Console.ReadKey();
        }
    }
}
