using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;
using System.Globalization;

namespace UI
{
    public class SystemSearchMenu : IMenu
    {
        public static Systems result = new Systems();
        private Systems filter = new Systems();
        public static bool forOrder = false;
        List<Systems> readout = new List<Systems>();
        private SystemsBL _systemBL;
        public bool noFilter = true;
        public SystemSearchMenu(SystemsBL p_systemBL)
        {
            _systemBL=p_systemBL;
        }
        public void Menu()
        {
            if (noFilter)
            {
                readout=_systemBL.GetAllSystems();
                Console.WriteLine("----System Search Menu----");
                Console.WriteLine("[0] to return to the product selection menu");
                Console.WriteLine("[1] to retrieve all consoles ("+readout.Count+")");
                Console.WriteLine("[2] to apply search filters");
            } else
            {
                Console.WriteLine("----System Search Menu----");
                Console.WriteLine("[0] to return to the product selection menu");
                Console.WriteLine("[1] to retrieve filtered list of consoles ("+readout.Count+")");
                Console.WriteLine("[2] to apply more search filters");
                Console.WriteLine("[3] to clear all search filters");
            }

        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.ProductSearchMenu;
                case "1":
                SystemList(readout);
                return MenuTitle.SystemSearchMenu;
                case "2":
                FilterSystems();
                noFilter=false;
                return MenuTitle.SystemSearchMenu;
                case "3":
                if (noFilter)
                {
                    return MenuTitle.Error;
                } else
                {
                    noFilter=true;
                    return MenuTitle.SystemSearchMenu;
                }
                default:
                return MenuTitle.Error;
            }
        }
        public void SystemList(List<Systems> reading)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Console List");
            foreach (Systems syst in reading)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine(i+".  Name: "+syst.sName +" | Release Date: "+ syst.sReleaseDate +" | MSRP: "+ syst.sMSRP);
                Console.WriteLine("=======================================");
                i++;
            }
            Console.WriteLine("End of list");
            Console.WriteLine("Enter 0 to return to search menu");
            if (forOrder)
            {
                Console.WriteLine("Enter the number of the system that is being ordered");
            }
            int choice = int.Parse(Console.ReadLine());
            if (choice==0)
            {
                Console.WriteLine("Returning to system search menu");
            } else if (choice<=reading.Count)
            {
                if (forOrder)
                {
                    //for the order / return make the customer object = to this one's result variable
                    result = reading[(choice-1)];
                }
                
            } else
            {
                Console.WriteLine("That input was not valid, please try again!");
            }
        }
    private void FilterSystems()
    {
        bool loop = true;
            while (loop)
            {
            Console.WriteLine("----System Filter Interface----");
            Console.WriteLine("Console: "+filter.sName);
            Console.WriteLine("Maximum Price: "+filter.sMSRP);
            Console.WriteLine("Released by: "+filter.sReleaseDate);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to remove filters and return to console search menu");
            Console.WriteLine("[1] to use the selected filters");
            Console.WriteLine("[2] to filter console name");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    noFilter=true;
                    loop=false;
                    filter=new Systems();
                    break;
                case "1":
                    loop=false;
                    readout=_systemBL.SearchSystems(filter.sName);
                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Console:");
                    filter.sName=Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("That input was not valid, please try again!");
                    break;
                }          
            }
        }
    }
}
