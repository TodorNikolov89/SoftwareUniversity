using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumptionInLittersPerkm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionInLittersPerkm + 1.6, tankCapacity)
        {
        }

        public override double TankCapacity
        {
            get => base.TankCapacity;
            set => base.TankCapacity = value;

        }
        public override void Refuel(double fuel)
        {
           
            fuel *= 0.95;
            base.Refuel(fuel);
        }
    }
}
