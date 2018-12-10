using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Vehicles
{
    public class Van : Vehicle
    {
        private const int defaulCapacity = 2;

        public Van() : base(defaulCapacity)
        {
        }
    }
}
