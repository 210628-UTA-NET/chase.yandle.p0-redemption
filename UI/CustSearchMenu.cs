using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;
using System.Globalization;

namespace UI
{
    public class CustSearchMenu : IMenu
    {
        public static Customers result = new Customers();
        private static Customers filter = new Customers();
        public static bool forOrder = false;
        List<Customers> readout = new List<Customers>();
        private CustomerBL _custBL;
        public static bool noFilter = true;
        private Systems cSystems = new Systems(); 
        public CustSearchMenu(CustomerBL p_custBL)
        {
            _custBL=p_custBL;
        }
        public void Menu()
        {
            if (noFilter)
            {
                readout=_custBL.GetAllCustomers();
                Console.WriteLine("----Customer Search / Edit Menu----");
                Console.WriteLine("[0] to return to the search menu");
                Console.WriteLine("[1] to retrieve all customers ("+readout.Count+")");
                Console.WriteLine("[2] to apply search filters");
            } else
            {
                readout=_custBL.FilterCustomer(filter);
                Console.WriteLine("----Customer Search / Edit Menu----");
                Console.WriteLine("[0] to return to the search menu");
                Console.WriteLine("[1] to retrieve filtered list of customers ("+readout.Count+")");
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
                return MenuTitle.SearchMenu;
                case "1":
                CustList(readout);
                return MenuTitle.CustomerSearchMenu;
                case "2":
                FilterCustomer();
                noFilter=false;
                return MenuTitle.CustomerSearchMenu;
                case "3":
                if (noFilter)
                {
                    return MenuTitle.Error;
                } else
                {
                    noFilter=true;
                    return MenuTitle.CustomerSearchMenu;
                }
                default:
                return MenuTitle.Error;
            }
        }
        public void CustList(List<Customers> reading)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Customer List");
            foreach (Customers cust in reading)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine(i+".  Name: "+cust.cName +" | Phone: "+ cust.cPhone +" | Email: "+ cust.cEmail);
                Console.WriteLine("=======================================");
                i++;
            }
            Console.WriteLine("End of list");
            Console.WriteLine("Enter 0 to return to search menu");
            if (!forOrder)
            {
                Console.WriteLine("Enter the number of the customer that you would like to edit");
            } else
            {
                Console.WriteLine("Enter the number of the customer that is placing this order");
            }
            int choice = int.Parse(Console.ReadLine());
            if (choice==0)
            {
                Console.WriteLine("Returning to customer search menu");
            } else if (choice<=reading.Count)
            {
                if (!forOrder)
                {
                    EditCustomer(reading[(choice-1)]);
                } else
                {
                    result = reading[(choice-1)];
                }
                
            } else
            {
                Console.WriteLine("That input was not valid, please try again!");
            }
        }

        public void EditCustomer(Customers toBeChanged)
        {
            bool loop = true;
            while (loop)
            {
            Console.WriteLine("----Customer Edit Interface----");
            Console.WriteLine("Customer ID: "+toBeChanged.cID);
            Console.WriteLine("Name: "+toBeChanged.cName);
            Console.WriteLine("Address: "+toBeChanged.cStreet+" "+toBeChanged.cCity+", "+toBeChanged.cState);
            Console.WriteLine("Phone Number: "+toBeChanged.cPhone);
            Console.WriteLine("Email: "+toBeChanged.cEmail);
            Console.WriteLine("Birthday: "+toBeChanged.cBDay.ToString("yyyy-MM-dd"));
            Console.WriteLine("Age: "+toBeChanged.ageNullIfZero);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to return to search menu");
            Console.WriteLine("[1] to submit customer changes to database");
            Console.WriteLine("[2] to edit customer name");
            Console.WriteLine("[3] to edit customer address");
            Console.WriteLine("[4] to edit customer phone number");
            Console.WriteLine("[5] to edit customer email");
            Console.WriteLine("[6] to edit customer age in format yyyy-MM-dd");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    loop = false;
                    break;
                case "1":
                    loop=false;
                    _custBL.EditCustomer(toBeChanged);
                    Console.WriteLine("Customer "+toBeChanged.cID+" Edited!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Name:");
                    toBeChanged.cName=Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Street:");
                    toBeChanged.cStreet=Console.ReadLine();
                    Console.WriteLine("City:");
                    toBeChanged.cCity=Console.ReadLine();
                    Console.WriteLine("State:");
                    toBeChanged.cState=Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("Phone Number:");
                    toBeChanged.cPhone=Console.ReadLine();
                    break;
                case "5":
                    Console.WriteLine("Email:");
                    toBeChanged.cEmail=Console.ReadLine();
                    break;
                case "6":
                    Console.WriteLine("Birthday in yyyy-MM-dd: ");
                    DateTime temp;
                    DateTime.TryParseExact(Console.ReadLine(),"yyyy-MM-dd",CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out temp);
                    toBeChanged.cBDay=temp;
                    break;
                default:
                    Console.WriteLine("That input was not valid, please try again!");
                    break;
            }          
        }
    }  
    private void FilterCustomer()
    {
        bool loop = true;
            while (loop)
            {
            Console.WriteLine("----Customer Filter Interface----");
            Console.WriteLine("Customer ID: "+filter.cID);
            Console.WriteLine("Name: "+filter.cName);
            Console.WriteLine("Address: "+filter.cStreet+" "+filter.cCity+", "+filter.cState);
            Console.WriteLine("Phone Number: "+filter.cPhone);
            Console.WriteLine("Email: "+filter.cEmail);
            Console.WriteLine("Birthday: "+filter.cBDay.ToString("yyyy-MM-dd"));
            Console.WriteLine("Age: "+filter.ageNullIfZero);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to remove filters and return to customer search menu");
            Console.WriteLine("[1] to use the selected filters");
            Console.WriteLine("[2] to filter customer name");
            Console.WriteLine("[3] to filter customer address");
            Console.WriteLine("[4] to filter customer phone number");
            Console.WriteLine("[5] to filter customer email");
            Console.WriteLine("[6] to filter customer age in format yyyy-MM-dd");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    noFilter=true;
                    loop=false;
                    filter=new Customers();
                    break;
                case "1":
                    loop=false;

                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Name:");
                    filter.cName=Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Street:");
                    filter.cStreet=Console.ReadLine();
                    Console.WriteLine("City:");
                    filter.cCity=Console.ReadLine();
                    Console.WriteLine("State:");
                    filter.cState=Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("Phone Number:");
                    filter.cPhone=Console.ReadLine();
                    break;
                case "5":
                    Console.WriteLine("Email:");
                    filter.cEmail=Console.ReadLine();
                    break;
                case "6":
                    Console.WriteLine("Birthday in MM-dd-yyyy: ");
                    DateTime temp;
                    DateTime.TryParseExact(Console.ReadLine(),"yyyy-MM-dd",CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out temp);
                    filter.cBDay=temp;
                    break;
                default:
                    Console.WriteLine("That input was not valid, please try again!");
                    break;
                }          
            }
        }
    }
}
