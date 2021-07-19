using System;
using System.Collections.Generic;
using BL;
using Models;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;

namespace UI
{
    public class CustOrderMenu : IMenu
    {

        private OrdersBL _ordersBL;
        private LineItemBL _lineitemBL;
        private static List<LineItems> orderLines= new List<LineItems>();
        public static string storeNumber=LoginMenu.storeID;
        public static string custo;
        public Orders order = new Orders();
        public string oNumber;
        private int i = 0;
        public int line = 1;
        public float subtotal=0;
        public float total=0;
        public StoresBL _storeBL;
        private DemoDbContext _options;
        public MenuFactory menuFactory1=new MenuFactory();
        public CustOrderMenu(OrdersBL p_orderBL, LineItemBL p_lineitemBL, DemoDbContext p_options,StoresBL p_store, CustomerBL p_cust)
        {
            _ordersBL=p_orderBL;
            _lineitemBL=p_lineitemBL;
            _options=p_options;
            _storeBL=p_store;
        }
        public void Menu()
        {
            Console.WriteLine("----Welcome to the customer order menu!----");
            Console.WriteLine("[0] to return to the order menu");
            Console.WriteLine("[1] to place the order");
            Console.WriteLine("[2] to search for the customer placing the order: "+custo);
            Console.WriteLine("[3] to search for the store the customer is ordering from: "+storeNumber);
            Console.WriteLine("[4] to search for a game to add to the order");
            Console.WriteLine("[5] to search for a console to add to the order");
            foreach (LineItems item in orderLines)
            {
                Console.WriteLine(i+".  "+item.liGame+item.liSystem+"  "+item.liPrice+"  "+item.liQuantity+"   "+item.liPrice*item.liQuantity);
                i++;
                subtotal=subtotal+item.liPrice*item.liQuantity;
            }
            i=0;
            Console.WriteLine("Total: "+subtotal.ToString());
            total=subtotal;
            subtotal=0;
        }
        public MenuTitle UInput()
        {
            MenuFactory menuFactory = new MenuFactory();
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.OrderMenu;
                case "1":
                order=AssignOrderFields(order);
                _ordersBL.AddOrders(order);
                _lineitemBL.AddLineItems(orderLines);
                _storeBL.SellStoreInventory(order.oStoreNumber,orderLines);
                Console.WriteLine("Order Made");
                Thread.Sleep(2000);
                order = new Orders();
                return MenuTitle.CustomerOrderMenu;
                case "2":
                CustSearchMenu.forOrder=true;
                CustSearchMenu custSearch = new CustSearchMenu(new CustomerBL(new CustRepository(_options)));
                custSearch.Menu();
                custSearch.UInput();
                custo=CustSearchMenu.result.cID;
                CustSearchMenu.forOrder=false;
                return MenuTitle.CustomerOrderMenu;
                case "3":
                LocSearchMenu.forOrder=true;
                LocSearchMenu sourceSearch = new LocSearchMenu(new StoresBL(new StoreRepository(_options)));
                sourceSearch.Menu();
                sourceSearch.UInput();
                storeNumber=LocSearchMenu.result.stNumber;
                LocSearchMenu.forOrder=false;
                return MenuTitle.CustomerOrderMenu;
                case "4":
                GameSearchMenu.forOrder=true;  
                LineItems game = new LineItems();
                GameSearchMenu gameSearch = new GameSearchMenu(new GamesBL(new GameRepository(_options)));
                gameSearch.Menu();
                gameSearch.UInput();
                game.liGame=GameSearchMenu.result.gName;
                game.liSystem=GameSearchMenu.result.gSystem;
                game=AssignDefaults(game);
                game.liPrice=GameSearchMenu.result.gMSRP;
                game.liLineNumber=line.ToString("000");
                line++;
                orderLines.Add(game);
                GameSearchMenu.forOrder=false;
                return MenuTitle.CustomerOrderMenu;
                case "5":
                SystemSearchMenu.forOrder=true;
                LineItems console = new LineItems();
                SystemSearchMenu systSearch = new SystemSearchMenu(new SystemsBL(new SystemRepository(_options)));
                systSearch.Menu();
                systSearch.UInput();
                console.liSystem=SystemSearchMenu.result.sName;
                console=AssignDefaults(console);
                console.liPrice=SystemSearchMenu.result.sMSRP;
                console.liLineNumber=line.ToString("000");
                line++;
                orderLines.Add(console);
                SystemSearchMenu.forOrder=false;
                return MenuTitle.CustomerOrderMenu;
                default:
                return MenuTitle.Error;
            }
        }
        public LineItems AssignDefaults(LineItems p_line)
        {
            Console.WriteLine("Please enter quantity to order:");
            p_line.liQuantity=int.Parse(Console.ReadLine());
            return p_line;
        } 

        public Orders AssignOrderFields(Orders p_orders)
        {
            int counting;
            string countString;
            order.oStoreNumber=storeNumber;
            order.oCustomerNumber=custo;
            counting = _ordersBL.FilterOrderBoth(order.oCustomerNumber, order.oStoreNumber).Count+1;
            counting++;
            countString=counting.ToString("0000");
            order.oNumber="C"+order.oCustomerNumber+countString;
            order.oDateAndTime=DateTime.Today;
            foreach (LineItems item in orderLines)
            {
                item.liLineNumber=order.oNumber+item.liLineNumber;
                item.liOrderNumber=order.oNumber;
            }
            
            return p_orders;
        }
    }
}