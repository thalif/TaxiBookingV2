using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxiBookingV2.Models;

namespace TaxiBookingV2.Vehicle
{
    public class Car : CabVehicle
    {
        public Car(string carName,LPOINT startLocation, double milagePerKMpm)
        {
            this.CarName = carName;
            this.IsAvailable = true;
            this.CurrentPoint = startLocation;
            this.KiloMeterPerMinutes = milagePerKMpm;
        }

        public string CarNumber { get; set; }
        public string CarName { get; set; }
        public double KiloMeterPerMinutes { get; set; }
        public FUEL FuelType { get; set; }
        public double BasePay{ get { return BasePayCalculation(); } }


        public override double BasePayCalculation()
        {
            double percentage = (this.KiloMeterPerMinutes - this.MarginMilage) * 100;
            return this.BasicPayPerKM + ((this.BasicPayPerKM / 100) * percentage);
        }
        //YES
        public override bool CheckAvailability()
        {
            // Get PickUp distance
            this.FairPickUpDistance = CurrentPoint - this.Raid.PickupPoint;
            // Get reaching time
            this.PickUpTimeSpan = this.Raid.PickUpTime - DateTime.Now;
            // Cab can reach distannce
            double cabReachingDistance = (double)this.KiloMeterPerMinutes * PickUpTimeSpan.Minutes;

            // if cab can reach to pickup location ? True : False
            return FairPickUpDistance <= cabReachingDistance;
        }
        public override double CalculateBill()
        {
            // Cab reached time 
            double TotalRaidTime = (int)(Convert.ToDouble(this.RaidDistanceKM) / this.KiloMeterPerMinutes);
            return TotalRaidTime * this.BasePay;
        }

        public override DateTime GetNextAvailTime()
        {
            throw new NotImplementedException();
        }

        public override Task StartRaid(Raid raid)
        {
            this.Raid = raid;
            double raidTime = CalculateRaidTime();

            this.IsAvailable = false;
            Console.WriteLine("Raid "+ this.CarName + " starts from " +raid.PickupPoint +" to "+ raid.DropPoint);
            return Task.Run(() => 
            {
                DateTime StartTime = DateTime.Now;
                this.IsAvailable = true;
                for (int i = 0; i < (int)raidTime; i++)
                {
                    Thread.Sleep(1000);
                }
                DateTime EndTime = DateTime.Now;
                this.CurrentPoint = this.Raid.DropPoint;
                this.BillAmount = CalculateBill();
                this.Earnings += BillAmount;
                
                Console.WriteLine("Raid " + this.CarName + " Ends at " + raid.DropPoint+ "\t: Timetaken = "+ (EndTime - StartTime).Seconds+" sec"+"\t"+this.BillAmount);                
            }); 
        }

        public override double CalculateRaidTime()
        {
            double pickUpDistance = this.Raid.PickupPoint - this.CurrentPoint;
            double raidDistance = Math.Abs(this.Raid.DropPoint - this.Raid.PickupPoint);
            double totalRaidDistance = pickUpDistance + raidDistance;
            return raidDistance / this.KiloMeterPerMinutes;
        }
    }
}
