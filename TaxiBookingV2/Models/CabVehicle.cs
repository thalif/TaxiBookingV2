using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiBookingV2.Models
{
    public abstract class CabVehicle
    {
        public LPOINT CurrentPoint { get; set; }
        public Raid Raid { get; set; }

        public double MarginMilage { get { return GetMarginMilage(); } }
        public double BasicPayPerKM
        {
            get { return GetBasePayOfCabService(); }
        }

        //
        public double BillAmount { get; set; }
        public double Earnings { get; set; }
        public bool IsAvailable { get; set; }

        //Action methods
        public abstract Task StartRaid(Raid raid);

        //Value Methods
        public DateTime NextAvailTime { get { return GetNextAvailTime(); } }
        public int FairPickUpDistance { get; set; }
        public TimeSpan PickUpTimeSpan { get; set; }
        public int RaidDistanceKM
        {
            get { return this.Raid.DropPoint - this.Raid.PickupPoint; }
        }

        //Abstract Methods
        public abstract double BasePayCalculation();
        public abstract DateTime GetNextAvailTime();
        public abstract bool CheckAvailability();
        //V2
        public abstract double CalculateRaidTime();
        public abstract double CalculateBill();


        // Methods to get Default Database values
        double GetBasePayOfCabService()
        {
            return 10;
        }
        double GetMarginMilage()
        {
            return 1;
        }
    }
}
