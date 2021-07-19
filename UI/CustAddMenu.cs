using System;
using Models;
using System.Collections.Generic;
using BL;
using System.Globalization;
using System.Threading;
using DL;

namespace UI
{
    public class CustAddMenu : IMenu
    {
        private CustomerBL _custBL;
        private static Customers cAdd = new Customers();
        private static Systems cSystems = new Systems();
        private SystemsBL _systBL;
        private List<Systems> allSystems = new List<Systems>();

        public CustAddMenu(CustomerBL p_custBL, SystemsBL p_systBL)
        {
            _custBL=p_custBL;
            _systBL = p_systBL;
        }

        public void Menu()
        {
            Console.WriteLine("----New Customer Input----");
            Console.WriteLine("Name: "+cAdd.cName);
            Console.WriteLine("Address: "+cAdd.cStreet+" "+cAdd.cCity+", "+cAdd.cState);
            Console.WriteLine("Phone Number: "+cAdd.cPhone);
            Console.WriteLine("Email: "+cAdd.cEmail);
            Console.WriteLine("Birthday: "+cAdd.cBDay.ToString("yyyy-MM-dd"));
            Console.WriteLine("Age: "+cAdd.ageNullIfZero);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to return to add menu");
            Console.WriteLine("[1] to add customer to database");
            Console.WriteLine("[2] to add customer name");
            Console.WriteLine("[3] to add customer address");
            Console.WriteLine("[4] to add customer phone number");
            Console.WriteLine("[5] to add customer email");
            Console.WriteLine("[6] to add customer age in format yyyy-MM-dd");
            Console.WriteLine("[7] to clear current customer data");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    ClearCustomer();
                    return MenuTitle.AddMenu;
                case "1":
                    cAdd.cStoreAddedAt=LoginMenu.storeID;
                    cAdd.cID=LoginMenu.storeID+(LoginMenu.customersAddedFrom).ToString("0000");
                    _custBL.AddCustomer(cAdd);
                    Console.WriteLine("Customer "+cAdd.cID+" Added!  Press Enter to Continue");
                    Console.ReadLine();
                    ClearCustomer();
                    return MenuTitle.CustomerAddMenu;
                case "2":
                    Console.WriteLine("Name:");
                    cAdd.cName=Console.ReadLine();
                    return MenuTitle.CustomerAddMenu;
                case "3":
                    Console.WriteLine("Street:");
                    cAdd.cStreet=Console.ReadLine();
                    Console.WriteLine("City:");
                    cAdd.cCity=Console.ReadLine();
                    Console.WriteLine("State:");
                    cAdd.cState=Console.ReadLine();
                    return MenuTitle.CustomerAddMenu;
                case "4":
                    Console.WriteLine("Phone Number:");
                    cAdd.cPhone=Console.ReadLine();
                    return MenuTitle.CustomerAddMenu;
                case "5":
                    Console.WriteLine("Email:");
                    cAdd.cEmail=Console.ReadLine();
                    return MenuTitle.CustomerAddMenu;
                case "6":
                    Console.WriteLine("Birthday in yyyy-MM-dd: ");
                    // cAdd.cBDay=Console.ReadLine();
                    DateTime temp;
                    DateTime.TryParseExact(Console.ReadLine(),"yyyy-MM-dd",CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out temp);
                    cAdd.cBDay=temp;
                    return MenuTitle.CustomerAddMenu; 
                case "7":
                    ClearCustomer();
                    return MenuTitle.CustomerAddMenu;
                default:
                    return MenuTitle.Error;
            }          
        }

        private void ClearCustomer()
        {
            cAdd=new Customers();
        }
        private void GetListOfSystems()
        {
            List<string> returning = new List<string>();
            allSystems = _systBL.GetAllSystems();
            foreach (Systems item in allSystems)
            {
                returning.Add(item.sName);
            }
            cSystems._availSystems=returning;
        }
    }
}