using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Vehicles
{
   public class Truck : Vehicle
    {
        private const int defaulCapacity = 5;

        public Truck() : base(defaulCapacity)
        {
        }
    }
}
