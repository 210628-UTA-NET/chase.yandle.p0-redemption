using System;
using BL;
using DL;
using System.IO;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UI
{
    public class MenuFactory
    {
        public IMenu CreateMenu(MenuTitle p_menu)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();
            string connectionString = configuration.GetConnectionString("Reference2DB");
            DbContextOptions<DemoDbContext> options = new DbContextOptionsBuilder<DemoDbContext>()
            .UseSqlServer(connectionString)
            .Options;
            
            switch(p_menu)
            {
                case MenuTitle.LoginMenu: 
                    LoginMenu.Login(options);
                    return new MainMenu();
                case MenuTitle.MainMenu: 
                    return new MainMenu();
                case MenuTitle.AddMenu: 
                    return new AddMenu();
                case MenuTitle.SearchMenu: 
                    return new SearchMenu();
                case MenuTitle.OrderMenu:
                    return new OrderMenu(); 
                case MenuTitle.CustomerAddMenu: 
                    return new CustAddMenu(new CustomerBL(new CustRepository(new DemoDbContext(options))), new SystemsBL(new SystemRepository(new DemoDbContext(options))));
                case MenuTitle.LocationAddMenu: 
                    return new LocAddMenu(new StoresBL( new StoreRepository(new DemoDbContext(options))));
                case MenuTitle.CustomerSearchMenu: 
                    return new CustSearchMenu(new CustomerBL(new CustRepository(new DemoDbContext(options))));
                case MenuTitle.LocationSearchMenu: 
                    return new LocSearchMenu(new StoresBL( new StoreRepository(new DemoDbContext(options))));
                case MenuTitle.ProductSearchMenu: 
                    return new ProdSelect();
                case MenuTitle.GameSearchMenu: 
                    return new GameSearchMenu(new GamesBL(new GameRepository(new DemoDbContext(options))));
                case MenuTitle.SystemSearchMenu: 
                    return new SystemSearchMenu(new SystemsBL(new SystemRepository(new DemoDbContext(options))));
                case MenuTitle.CustomerOrderMenu: 
                    return new CustOrderMenu(new OrdersBL(new OrdRepository(new DemoDbContext(options))), new LineItemBL(new LineRepository(new DemoDbContext(options))), new DemoDbContext(options), new StoresBL( new StoreRepository(new DemoDbContext(options))), new CustomerBL(new CustRepository(new DemoDbContext(options))));
                case MenuTitle.StockOrderMenu: 
                    return new StockOrderMenu(new StockOrdersBL(new StockOrdRepository(new DemoDbContext(options))),new LineItemBL(new LineRepository(new DemoDbContext(options))),new DemoDbContext(options),new StoresBL( new StoreRepository(new DemoDbContext(options))));
                case MenuTitle.StockOrderSearchMenu: 
                    return new StockOrdSearchMenu(new StockOrdersBL(new StockOrdRepository(new DemoDbContext(options))), new StoresBL( new StoreRepository(new DemoDbContext(options))), new LineItemBL(new LineRepository(new DemoDbContext(options))));
                case MenuTitle.CustomerOrderSearchMenu: 
                    return new CustOrdSearchMenu(new OrdersBL(new OrdRepository(new DemoDbContext(options))), new LineItemBL(new LineRepository(new DemoDbContext(options))));
                default:
                    return null;
            }
        }
    }
}