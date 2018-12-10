using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Products
{
    public class Gpu : Product
    {
        private const double gpuWeight = 0.7;

        public Gpu(double price) : base(price, gpuWeight)
        {
        }
    }
}
