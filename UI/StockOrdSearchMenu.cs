using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;

namespace UI
{
    public class StockOrdSearchMenu : IMenu
    {
        public static StockOrders result = new StockOrders();
        private StockOrders filter = new StockOrders();
        List<StockOrders> readout = new List<StockOrders>();
        private StockOrdersBL _orderBL;
        private LineItemBL _lineItemBL;
        public bool noFilter = true;
        public StockOrdSearchMenu(StockOrdersBL p_orderBL, StoresBL p_storeBL, LineItemBL p_lineItemBL)
        {
            _orderBL=p_orderBL;
            _lineItemBL=p_lineItemBL;
        }
        public void Menu()
        {
            if (noFilter)
            {
                readout=_orderBL.GetAllStockOrders();
                Console.WriteLine("----Stock Order Search Menu----");
                Console.WriteLine("[0] to return to the order selection menu");
                Console.WriteLine("[1] to retrieve all Stock orders ("+readout.Count+")");
                Console.WriteLine("[2] to apply search filters");
            } else
            {
                Console.WriteLine("----Order Search Menu----");
                Console.WriteLine("[0] to return to the order selection menu");
                Console.WriteLine("[1] to retrieve filtered list of Stock orders ("+readout.Count+")");
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
                StockOrderList(readout);
                return MenuTitle.StockOrderSearchMenu;
                case "2":
                FilterStockOrders();
                noFilter=false;
                return MenuTitle.StockOrderSearchMenu;
                case "3":
                if (noFilter)
                {
                    return MenuTitle.Error;
                } else
                {
                    noFilter=true;
                    return MenuTitle.StockOrderSearchMenu;
                }
                default:
                return MenuTitle.Error;
            }
        }
        public void StockOrderList(List<StockOrders> reading)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Stock Order List");
            foreach (StockOrders ord in reading)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine(i+".  Order ID#: "+ord.soNumber +" | Destination: "+ ord.soDestination +" | Source: "+ ord.soSource);
                Console.WriteLine("=======================================");
                i++;
            }
            Console.WriteLine("End of list");
            Console.WriteLine("Enter 0 to return to search menu");

                Console.WriteLine("Enter the number of the stock order that you would like to view");
            int choice = int.Parse(Console.ReadLine());
            if (choice==0)
            {
                Console.WriteLine("Returning to stock order search menu");
            } else if (choice<=reading.Count)
            {

                
                ViewStockOrder(reading[(choice-1)]);
                
            } else
            {
                Console.WriteLine("That input was not valid, please try again!");
            }
        }

        public void ViewStockOrder(StockOrders toBeViewed)
        {

            int i=1;
            Console.WriteLine("----Order Viewing Interface----");
            Console.WriteLine("Order ID: "+toBeViewed.soNumber);
            Console.WriteLine("Destination Store ID: "+toBeViewed.soDestination);
            Console.WriteLine("Source Store ID: "+toBeViewed.soSource);
            Console.WriteLine("Date and Time: "+toBeViewed.soRequestTime);
            List<LineItems> _lineItem = new List<LineItems>();
                _lineItem.AddRange(_lineItemBL.GetSystemsLineItemsFromOrder(toBeViewed));
                _lineItem.AddRange(_lineItemBL.GetGameLineItemsFromOrder(toBeViewed));
                foreach (LineItems items in _lineItem)
                {
                    Console.WriteLine(i+".  "+items.liGame+" "+items.liSystem+"   "+items.liQuantity);
                    i++;
                }
            i=1;
            Console.WriteLine("---------------------------");
            Console.WriteLine("Press enter to return to search menu");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            
        
    }        

    private void FilterStockOrders()
    {
        string gameTarget="";
        string systemTarget="";
        bool loop = true;
            while (loop)
            {
            Console.WriteLine("----Order Filter Interface----");
            Console.WriteLine("Order Number: "+filter.soNumber);
            Console.WriteLine("Customer Number: "+filter.soDestination);
            Console.WriteLine("Store Number: "+filter.soSource);
            Console.WriteLine("Game: "+gameTarget);
            Console.WriteLine("Console: "+systemTarget);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to remove filters and return to stock order search menu");
            Console.WriteLine("[1] to filter by Destination Store Number");
            Console.WriteLine("[2] to filter by Source Store Number");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    noFilter=true;
                    loop=false;
                    filter=new StockOrders();
                    break;
                case "1":
                    loop=false;
                    readout=_orderBL.FilterStockOrderDestination(filter.soDestination);
                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Order Number:");
                    filter.soNumber=Console.ReadLine();
                    loop=false;
                    readout=_orderBL.FilterStockOrderSource(filter.soSource);
                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Customer Number:");
                    filter.soDestination=Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("Store Number:");
                    filter.soSource=Console.ReadLine();
                    break;
                case "5":
                    Console.WriteLine("Product to search for:");
                    gameTarget=Console.ReadLine();
                    break;
                case "6":
                    Console.WriteLine("Product to search for:");
                    systemTarget=Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("That input was not valid, please try again!");
                    break;
                }          
            }
        }
    }
}
