using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        public Car(double fuelQuantity, double fuelConsumptionInLittersPerkm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionInLittersPerkm + 0.9, tankCapacity)
        {

        }

    }
}
