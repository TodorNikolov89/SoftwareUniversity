using GrandPrix.Models.Cars;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandPrix.Models.Drivers
{
    public abstract class Driver
    {
        //Driver’s Speed is calculated throught the formula below.Keep in mind that Speed changes on each lap.
        //Speed = “(car’s Hp + tyre’s degradation) / car’s fuelAmount”

        private double speed;
        private double fuelConsumptionPerKm;
        private string name;
        private double totalTime;
        private Car car;

        public Car Car
        {
            get { return car; }
            set { car = value; }
        }

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public double FuelConsumptionPerKm
        {
            get { return fuelConsumptionPerKm; }
            set { fuelConsumptionPerKm = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public double TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

    }
}
