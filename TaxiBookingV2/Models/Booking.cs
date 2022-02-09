using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiBookingV2.Vehicle;

namespace TaxiBookingV2.Models
{
    public class Booking
    {
        public Booking()
        {
            StartGarage();
        }
        static List<CabVehicle> CAB_GARAGE = new List<CabVehicle>();
        static void StartGarage()
        {
            CAB_GARAGE.Add(new Car("BMW X3",LPOINT.A, 5));
            CAB_GARAGE.Add(new Car("I20", LPOINT.A, 1.2));
            CAB_GARAGE.Add(new Car("POLO", LPOINT.A, 2));
            CAB_GARAGE.Add(new Car("SWIFT", LPOINT.A, 1.34));
            CAB_GARAGE.Add(new Car("SKODA", LPOINT.A, 1.6));
        }


        public async void BookVehicle(Raid raid)
        {
            CabVehicle vehicle = GetVehicleForThisRaid(raid);
            await Task.Run(() => vehicle.StartRaid(raid));
        }

        static CabVehicle GetVehicleForThisRaid(Raid raid)
        {
            List<CabVehicle> Avails = CAB_GARAGE.Where(o => o.IsAvailable).Select(o => o).ToList();
            List<CabVehicle> NearestVehicle = GetNearestVehicle(Avails, raid.PickupPoint);
            CabVehicle vehicle = GetLowEarnigsVehicle(NearestVehicle);
            return vehicle;
        }
        static List<CabVehicle> GetNearestVehicle(List<CabVehicle> avails, LPOINT pickPoint)
        {
            List<CabVehicle> closest = new List<CabVehicle>();

            int LowDistance = int.MaxValue;
            foreach (CabVehicle vehicle in avails)
            {
                int distance = Math.Abs(pickPoint - vehicle.CurrentPoint);
                if (distance < LowDistance)
                {
                    LowDistance = distance;
                    closest.Clear();
                    closest.Add(vehicle);
                }
                else if (distance == LowDistance)
                {
                    closest.Add(vehicle);
                }
            }
            return closest;
        }
        static CabVehicle GetLowEarnigsVehicle(List<CabVehicle> nearestVehicle)
        {
            List<CabVehicle> lowEarningsVehicle = new List<CabVehicle>();

            double LowEarnigns = int.MinValue;
            foreach (CabVehicle vehicle in nearestVehicle)
            {
                if (LowEarnigns < vehicle.Earnings)
                {
                    LowEarnigns = vehicle.Earnings;
                    lowEarningsVehicle.Clear();
                    lowEarningsVehicle.Add(vehicle);
                }
                else if (LowEarnigns == vehicle.Earnings)
                {
                    lowEarningsVehicle.Add(vehicle);
                }
            }

            if (lowEarningsVehicle.Count() > 1)
            {
                Random rand = new Random();
                int randNo = rand.Next(0,lowEarningsVehicle.Count());

                return nearestVehicle[randNo];
            }
            return nearestVehicle[0];
        }
    }
}
