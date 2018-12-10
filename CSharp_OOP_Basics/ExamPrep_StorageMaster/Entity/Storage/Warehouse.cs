using StorageMaster.Entity.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Storages
{
    public class Warehouse : Storage
    {
        private static int WarehouseCapacity = 10;
        private static int WarehouseGarageSlots = 10;

        private static Vehicle[] defaultVehicles = new Vehicle[]
        {
            new Semi(),
            new Semi(),
            new Semi()
        };

        public Warehouse(string name)
            : base(name, WarehouseCapacity, WarehouseGarageSlots, defaultVehicles)
        {
        }
    }
}
