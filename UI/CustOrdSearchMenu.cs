using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;

namespace UI
{
    public class CustOrdSearchMenu : IMenu
    {
        public static Orders result = new Orders();
        private Orders filter = new Orders();
        List<Orders> readout = new List<Orders>();
        private OrdersBL _orderBL;
        private LineItemBL _lineBL;
        public bool noFilter = true;
        public CustOrdSearchMenu(OrdersBL p_orderBL, LineItemBL p_lineBL)
        {
            _orderBL=p_orderBL;
            _lineBL=p_lineBL;
        }
        public void Menu()
        {
            if (noFilter)
            {
                readout=_orderBL.GetAllOrders();
                Console.WriteLine("----Order Search Menu----");
                Console.WriteLine("[0] to return to the order selection menu");
                Console.WriteLine("[1] to retrieve all orders ("+readout.Count+")");
                Console.WriteLine("[2] to apply search filters");
            } else
            {
                Console.WriteLine("----Order Search Menu----");
                Console.WriteLine("[0] to return to the order selection menu");
                Console.WriteLine("[1] to retrieve filtered list of orders ("+readout.Count+")");
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
                OrderList(readout);
                return MenuTitle.CustomerOrderSearchMenu;
                case "2":
                FilterOrders();
                noFilter=false;
                return MenuTitle.CustomerOrderSearchMenu;
                case "3":
                if (noFilter)
                {
                    return MenuTitle.Error;
                } else
                {
                    noFilter=true;
                    return MenuTitle.CustomerOrderSearchMenu;
                }
                default:
                return MenuTitle.Error;
            }
        }
        public void OrderList(List<Orders> reading)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Order List");
            foreach (Orders ord in reading)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine(i+".  Order ID#: "+ord.oNumber +" | Price: "+ ord.oTotalPrice +" | Customer: "+ ord.oCustomerNumber);
                Console.WriteLine("=======================================");
                i++;
            }
            Console.WriteLine("End of list");
            Console.WriteLine("Enter 0 to return to search menu");

                Console.WriteLine("Enter the number of the order that you would like to view");
            int choice = int.Parse(Console.ReadLine());
            if (choice==0)
            {
                Console.WriteLine("Returning to customer search menu");
            } else if (choice<=reading.Count)
            {

                
                ViewOrder(reading[(choice-1)]);
                
            } else
            {
                Console.WriteLine("That input was not valid, please try again!");
            }
        }

        public void ViewOrder(Orders toBeViewed)
        {
            int i=1;
            List<LineItems> _lineItems= new List<LineItems>();
            Console.WriteLine("----Order Viewing Interface----");
            Console.WriteLine("Order ID: "+toBeViewed.oNumber);
            Console.WriteLine("Customer Number: "+toBeViewed.oCustomerNumber);
            Console.WriteLine("Store Number: "+toBeViewed.oStoreNumber);
            Console.WriteLine("Date and Time: "+toBeViewed.oDateAndTime);
            
                _lineItems.AddRange(_lineBL.GetSystemsLineItemsFromOrder(toBeViewed));
                _lineItems.AddRange(_lineBL.GetGameLineItemsFromOrder(toBeViewed));
                foreach (LineItems item in _lineItems)
                {
                    Console.WriteLine(i+".  "+item.liGame+" "+item.liSystem+"   "+item.liQuantity);
                    i++;
                }  
            i=1;
            Console.WriteLine("Total Price: "+toBeViewed.oTotalPrice);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Press enter to return to search menu");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
        }        

    private void FilterOrders()
    {
        string gameTarget="";
        string systemTarget="";
        bool loop = true;
            while (loop)
            {
            Console.WriteLine("----Order Filter Interface----");
            Console.WriteLine("Order Number: "+filter.oNumber);
            Console.WriteLine("Customer Number: "+filter.oCustomerNumber);
            Console.WriteLine("Store Number: "+filter.oStoreNumber);
            Console.WriteLine("Game: "+gameTarget);
            Console.WriteLine("Console: "+systemTarget);
            Console.WriteLine("Total Price: "+filter.oTotalPrice);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to remove filters and return to customer order search menu");
            Console.WriteLine("[1] to filter Customer Number");
            Console.WriteLine("[2] to filter Store Number");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    noFilter=true;
                    loop=false;
                    filter=new Orders();
                    break;
                case "1":
                    Console.WriteLine("Customer Number:");
                    filter.oCustomerNumber=Console.ReadLine();
                    loop=false;
                    readout=_orderBL.FilterOrderByCust(filter.oCustomerNumber);
                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Store Number:");
                    filter.oStoreNumber=Console.ReadLine();
                    loop=false;
                    readout=_orderBL.FilterOrdeByStore(filter.oStoreNumber);
                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("That input was not valid, please try again!");
                    break;
                }          
            }
        }
    }
}
