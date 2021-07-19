using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;
using Serilog;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using DL.Entities;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            MenuFactory menuFactory = new MenuFactory();
            menuFactory.CreateMenu(MenuTitle.LoginMenu);
            IMenu newMenu = new MainMenu();
            bool loop = true;
            MenuTitle location = MenuTitle.MainMenu;
            while(loop)
            {
                Console.Clear();
                newMenu.Menu();
                location = newMenu.UInput();

                switch(location)
                {
                    case MenuTitle.MainMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.CustomerAddMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.CustomerSearchMenu:
                    CustSearchMenu.forOrder=false;
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.LoginMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.Exit:
                    Console.Clear();
                    SplashScreen.SplashMessage();
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using this program!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    loop=false;
                    break;
                    case MenuTitle.AddMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.SearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.OrderMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.LocationAddMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.LocationSearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.ProductSearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.GameSearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.SystemSearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.StockOrderSearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.CustomerOrderSearchMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.CustomerOrderMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;
                    case MenuTitle.StockOrderMenu:
                    newMenu = menuFactory.CreateMenu(location);
                    break;                 
                    default:
                    Console.WriteLine("That input was not valid, please try again!");
                    Thread.Sleep(2000);
                    break;
                    
                }
            }
        }
        public static string StringListOneLine(List<string> items)
        {
            return string.Join<string>(", ",items);
        }
    }
}
