using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;

namespace UI
{
    public class LocSearchMenu : IMenu
    {
        public static Stores result = new Stores();
        private Stores filter = new Stores();
        public static bool forOrder = false;
        List<Stores> readout = new List<Stores>();
        private StoresBL _storeBL;
        private Systems cSystems = new Systems(); 
        public LocSearchMenu(StoresBL p_storeBL)
        {
            _storeBL=p_storeBL;
        }
        public void Menu()
        {
                readout=_storeBL.GetAllStores();
                Console.WriteLine("----Store Search / Edit Menu----");
                Console.WriteLine("[0] to return to the search menu");
                Console.WriteLine("[1] to retrieve all stores ("+readout.Count+")");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.SearchMenu;
                case "1":
                StoreList(readout);
                return MenuTitle.LocationSearchMenu;
                case "2":
                return MenuTitle.LocationSearchMenu;
                default:
                return MenuTitle.Error;
            }
        }
        public void StoreList(List<Stores> reading)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Store List");
            foreach (Stores store in reading)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine(i+".  Store#: "+store.stNumber +" | Phone: "+ store.stPhone +" | Email: "+ store.stEmail);
                Console.WriteLine("=======================================");
                i++;
            }
            Console.WriteLine("End of list");
            Console.WriteLine("Enter 0 to return to search menu");
            if (!forOrder)
            {
                Console.WriteLine("Enter the number of the store that you would like to edit");
            } else
            {
                Console.WriteLine("Enter the number of the store that is being used in this order");
            }
            int choice = int.Parse(Console.ReadLine());
            if (choice==0)
            {
                Console.WriteLine("Returning to store search menu");
            } else if (choice<=reading.Count)
            {
                if (!forOrder)
                {
                    EditStore(reading[(choice-1)]);
                } else
                {
                    result = reading[(choice-1)];
                }
                
            } else
            {
                Console.WriteLine("That input was not valid, please try again!");
            }
        }

        public void EditStore(Stores toBeChanged)
        {
            {
            Console.WriteLine("----Store Viewing Interface----");
            Console.WriteLine("Store ID: "+toBeChanged.stNumber);
            Console.WriteLine("Address: "+toBeChanged.stStreet+" "+toBeChanged.stCity+", "+toBeChanged.stState);
            Console.WriteLine("Phone Number: "+toBeChanged.stPhone);
            Console.WriteLine("Email: "+toBeChanged.stEmail);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Store Console Inventory");
            foreach (LineItems item in _storeBL.SystemStoreInventory(toBeChanged))
            {
                Console.WriteLine(item.liSystem+"     "+item.liQuantity);
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("Store Game Inventory");
            foreach (LineItems item in _storeBL.GamesStoreInventory(toBeChanged))
            {
                Console.WriteLine(item.liGame+"     "+item.liQuantity);
            }
            Console.WriteLine("[0] to return to search menu");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            }          
        }
    }
}
