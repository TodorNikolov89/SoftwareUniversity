namespace Travel.Entities.Factories
{
    using Contracts;
    using Airplanes.Contracts;
    using Travel.Entities.Airplanes;
    using System.Reflection;
    using System;
    using System.Linq;

    public class AirplaneFactory : IAirplaneFactory
    {
        public IAirplane CreateAirplane(string type)
        {
           // IAirplane airplane = null;
            //switch (type)
            //{
            //    case "LightAirplane":
            //        airplane = new LightAirplane();
            //        break;
            //    case "MediumAirplane":
            //        airplane = new MediumAirplane();
            //        break;
            //}
            //return airplane;

            var allTypes = Assembly.GetCallingAssembly().GetTypes();

            var setType = allTypes
                .Where(t => typeof(IAirplane).IsAssignableFrom(t))
                .FirstOrDefault(t => t.Name == type);

            var set = (IAirplane)Activator.CreateInstance(setType);

            return set;
        }
    }
}