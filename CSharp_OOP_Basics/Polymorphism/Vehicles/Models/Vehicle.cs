using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        private double fuelConsumptionInLittersPerkm;
        private double tankCapacity;

        protected Vehicle(double fuelQuantity, double fuelConsumptionInLittersPerkm, double tankCapacity)
        {
            this.FuelQuantity = fuelQuantity;
            this.fuelConsumptionInLittersPerkm = fuelConsumptionInLittersPerkm;
            this.TankCapacity = tankCapacity;

        }

        public virtual double TankCapacity
        {
            get
            {
                return tankCapacity;
            }
            set
            {
                tankCapacity = value;
            }
        }


        public string Drive(double distance)
        {
            if (this.FuelQuantity >= distance * this.fuelConsumptionInLittersPerkm)
            {
                this.FuelQuantity -= this.fuelConsumptionInLittersPerkm * distance;
                return $"{this.GetType().Name} travelled {distance} km";
            }

            return $"{this.GetType().Name} needs refueling";
        }

        public double FuelQuantity
        {
            get
            {
                return fuelQuantity;
            }
            set
            {
                if (value > this.TankCapacity)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }


        public virtual void Refuel(double fuel)
        {
            double freeCapacity = GetFreeCapacity(this.TankCapacity, this.fuelQuantity);

            if (freeCapacity < fuel)
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
            }
            else
            {
                this.fuelQuantity += fuel;
            }

        }

        private double GetFreeCapacity(double tankCapacity, double fuelQuantity)
        {
            return tankCapacity - fuelQuantity;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.fuelQuantity:f2}";
        }
    }
}
