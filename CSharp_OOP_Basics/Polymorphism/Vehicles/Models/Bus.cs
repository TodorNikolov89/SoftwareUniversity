using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        public Bus(double fuelQuantity, double fuelConsumptionInLittersPerkm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionInLittersPerkm, tankCapacity)
        {
        }


    }
}
