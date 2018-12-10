using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Models.Procedures
{
    public abstract class Procedure : IProcedure
    {
        protected Dictionary<string, List<IAnimal>> procedureHistory;

        public Procedure()
        {
            procedureHistory = new Dictionary<string, List<IAnimal>>();
        }

        public IReadOnlyDictionary<string, List<IAnimal>> ProcedureHistory => this.procedureHistory;

        //TODO method
        public abstract void DoService(IAnimal animal, int procedureTime);

        public virtual string History()
        {
            string type = this.GetType().Name;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{type}");

            foreach (var animal in this.ProcedureHistory[type])
            {
                sb.AppendLine($"    - {animal.Name} - Happiness: {animal.Happiness} - Energy: {animal.Energy}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        public virtual bool CheckProcedureTime(int procedureTime, int procedureNeededTIme)
        {
            if (procedureTime < procedureNeededTIme)
            {
                return true;
            }

            return false;
        }
        
        
    }
}
