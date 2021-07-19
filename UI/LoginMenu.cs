using System;
using Models;
using System.Globalization;
using BL;
using DL;
using System.Collections.Generic;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace UI
{
    public class LoginMenu
    {
        public static string storeID="0000";
        public static Stores hub = new Stores();
        public static int customersAddedFrom;
        public static StoresBL store;
        public static void Login(DbContextOptions<DemoDbContext> p_options)
        {
            Console.Clear();
            store = new StoresBL(new StoreRepository(new DemoDbContext(p_options)));
            Console.WriteLine("----Welcome to the Gameshop Storefront Application----");
            Console.WriteLine("Please enter the store number or enter 0 for a list of storefronts:");
            string selection = Console.ReadLine();
            if (selection=="0")
            {
                StoreList();
                SplashScreen.SplashMessage();
            } else try
            {
               
                hub=store.GetStoreByNumber(selection);
                storeID=hub.stNumber;
                customersAddedFrom = store.CountCustomers(hub);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Please enter a valid store number");
                    Thread.Sleep(2000);
                    Console.Clear();
                    LoginMenu.Login(p_options);
            }
            
        }

        private static void StoreList()
        {
            int i = 1;
            List<Stores> storeList = store.GetAllStores();
            foreach (Stores item in storeList)
            {
                Console.WriteLine("["+i+"]  Store: "+item.stNumber+"   City: "+item.stCity);
                i++;
            }
            Console.WriteLine("Please enter the number of the store you would like to use");
            int choice = int.Parse(Console.ReadLine());
            if (choice>0 && choice<=storeList.Count)
            {
                storeID = storeList[choice-1].stNumber;
            }
            else
            {
                Console.WriteLine("Please enter a valid store number");
            }
        }
    }
}