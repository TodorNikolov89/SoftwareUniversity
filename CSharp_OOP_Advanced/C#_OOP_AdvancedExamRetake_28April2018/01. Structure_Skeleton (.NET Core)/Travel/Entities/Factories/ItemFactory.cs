namespace Travel.Entities.Factories
{
	using Contracts;
	using Items;
	using Items.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class ItemFactory : IItemFactory
	{
		public IItem CreateItem(string type)
		{
            //         IItem item = null;
            //switch (type)
            //{
            //	case "CellPhone":
            //		item =  new CellPhone();
            //                 break;
            //	case "Colombian":
            //                 item = new Colombian();
            //                 break;
            //             case "Jewelery":
            //                 item = new Jewelery();
            //                 break;
            //             case "Laptop":
            //                 item = new Laptop();
            //                 break;
            //             case "Toothbrush":
            //                 item = new Toothbrush();
            //                 break;
            //             case "TravelKit":
            //                 item = new TravelKit();
            //                 break;
            //             default:
            //                 break;
            //}
            //         return item;
            var allTypes = Assembly.GetCallingAssembly().GetTypes();

            var setType = allTypes
                .Where(t => typeof(IItem).IsAssignableFrom(t))
                .FirstOrDefault(t => t.Name == type);

            var set = (IItem)Activator.CreateInstance(setType);

            return set;
        }
	}
}
