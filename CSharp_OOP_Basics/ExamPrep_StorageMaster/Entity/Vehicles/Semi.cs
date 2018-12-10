using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Vehicles
{
    public class Semi : Vehicle
    {
        private const int defaulCapacity = 5;

        public Semi() : base(defaulCapacity)
        {
        }
    }
}
