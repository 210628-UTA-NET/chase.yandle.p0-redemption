using System;
using Models;
using System.Collections.Generic;
using BL;
using System.Globalization;
using System.Threading;

namespace UI
{
    public class LocAddMenu : IMenu
    {
        private StoresBL _storeBL;
        private static Stores lAdd = new Stores();

        public LocAddMenu(StoresBL p_locBL)
        {
            _storeBL=p_locBL;
        }

        public void Menu()
        {
            Console.WriteLine("----New Store Input----");
            Console.WriteLine("Address: "+lAdd.stStreet+" "+lAdd.stCity+", "+lAdd.stState);
            Console.WriteLine("Phone Number: "+lAdd.stPhone);
            Console.WriteLine("Email: "+lAdd.stEmail);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to return to add menu");
            Console.WriteLine("[1] to add store to database");
            Console.WriteLine("[2] to add store address");
            Console.WriteLine("[3] to add store phone number");
            Console.WriteLine("[4] to add store email");
            Console.WriteLine("[5] to clear current store data");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    ClearStore();
                    return MenuTitle.AddMenu;
                case "1":
                    int number = _storeBL.GetAllStores().Count;
                    lAdd.stNumber=(number+1).ToString("0000");
                    _storeBL.AddStore(lAdd);
                    Console.WriteLine("Store "+lAdd.stNumber+" Added!  Press Enter to Continue");
                    Console.ReadLine();
                    ClearStore();
                    return MenuTitle.LocationAddMenu;
                case "2":
                    Console.WriteLine("Street:");
                    lAdd.stStreet=Console.ReadLine();
                    Console.WriteLine("City:");
                    lAdd.stCity=Console.ReadLine();
                    Console.WriteLine("State:");
                    lAdd.stState=Console.ReadLine();
                    return MenuTitle.LocationAddMenu;
                case "3":
                    Console.WriteLine("Phone Number:");
                    lAdd.stPhone=Console.ReadLine();
                    return MenuTitle.LocationAddMenu;
                case "4":
                    Console.WriteLine("Email:");
                    lAdd.stEmail=Console.ReadLine();
                    return MenuTitle.LocationAddMenu;
                case "5":
                    ClearStore();
                    return MenuTitle.LocationAddMenu;
                default:
                    return MenuTitle.Error;
            }          
        }
        private void ClearStore()
        {
            lAdd=new Stores();
        }
    }
}