using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyPattern.Contracts
{
    public interface IPerson
    {
        string Name { get; }

        int Age { get; }
    }
}
