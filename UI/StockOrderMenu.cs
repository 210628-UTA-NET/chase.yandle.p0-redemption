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
    public class StockOrderMenu : IMenu
    {

        private StockOrdersBL _stockordersBL;
        private LineItemBL _lineitemBL;
        private static List<LineItems> orderLines= new List<LineItems>();
        public static string source="0000";
        public static string destination=LoginMenu.storeID;
        public StockOrders order = new StockOrders();
        public string oNumber;
        private int i = 0;
        public int line = 1;
        public float subtotal=0;
        public float total=0;
        public StoresBL _storeBL;
        private DemoDbContext _options;
        public MenuFactory menuFactory1=new MenuFactory();
        public StockOrderMenu(StockOrdersBL p_stockordersBL, LineItemBL p_lineitemBL, DemoDbContext p_options,StoresBL p_store)
        {
            _stockordersBL=p_stockordersBL;
            _lineitemBL=p_lineitemBL;
            _options=p_options;
            _storeBL=p_store;
        }
        public void Menu()
        {
            Console.WriteLine("----Welcome to the stock order menu!----");
            Console.WriteLine("[0] to return to the order menu");
            Console.WriteLine("[1] to place the order");
            Console.WriteLine("[2] to search for the store placing the order: "+destination);
            Console.WriteLine("[3] to search for the store the requestor is ordering from: "+source);
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
                _stockordersBL.AddStockOrders(order);
                _lineitemBL.AddLineItems(orderLines);
                _storeBL.AddStoreInventory(order.soDestination, orderLines);
                _storeBL.SellStoreInventory(order.soSource,orderLines);
                Console.WriteLine("Order Made");
                Thread.Sleep(2000);
                order = new StockOrders();
                return MenuTitle.StockOrderMenu;
                case "2":
                LocSearchMenu.forOrder=true;
                LocSearchMenu destSearch = new LocSearchMenu(new StoresBL(new StoreRepository(_options)));
                destSearch.Menu();
                destSearch.UInput();
                destination=LocSearchMenu.result.stNumber;
                LocSearchMenu.forOrder=false;
                return MenuTitle.StockOrderMenu;
                case "3":
                LocSearchMenu.forOrder=true;
                LocSearchMenu sourceSearch = new LocSearchMenu(new StoresBL(new StoreRepository(_options)));
                sourceSearch.Menu();
                sourceSearch.UInput();
                source=LocSearchMenu.result.stNumber;
                LocSearchMenu.forOrder=false;
                return MenuTitle.StockOrderMenu;
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
                return MenuTitle.StockOrderMenu;
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
                return MenuTitle.StockOrderMenu;
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

        public StockOrders AssignOrderFields(StockOrders p_orders)
        {
            int counting;
            string countString;
            order.soSource=source;
            order.soDestination=destination;
            counting = _stockordersBL.FilterStockOrderBoth(order.soDestination, order.soSource).Count+1;
            counting++;
            countString=counting.ToString("0000");
            order.soNumber="S"+order.soSource+order.soDestination+countString;
            order.soRequestTime=DateTime.Today;
            foreach (LineItems item in orderLines)
            {
                item.liLineNumber=order.soNumber+item.liLineNumber;
                item.liOrderNumber=order.soNumber;
            }
            
            return p_orders;
        }
    }
}