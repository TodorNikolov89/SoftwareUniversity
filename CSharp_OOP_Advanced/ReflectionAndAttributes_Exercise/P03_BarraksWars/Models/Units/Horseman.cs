using _03BarracksFactory.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_BarraksWars.Models.Units
{
    public class Horseman : Unit
    {
        private const int DefaultHealth = 30;
        private const int DefaultDamage = 15;

        protected Horseman()
            : base(DefaultHealth, DefaultDamage)
        {
        }
    }
}
