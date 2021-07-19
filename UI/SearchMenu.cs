using System;

namespace UI
{
    public class SearchMenu : IMenu
    {
        public void Menu()
        {
            Console.WriteLine("----Welcome to the searching and editing menu!----");
            Console.WriteLine("[0] to return to the main menu");
            Console.WriteLine("[1] to search for / edit a customer");
            Console.WriteLine("[2] to search for / edit a store");
            Console.WriteLine("[3] to search for a product");
            Console.WriteLine("[4] to search customer orders");
            Console.WriteLine("[5] to search stock orders");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.MainMenu;
                case "1":
                return MenuTitle.CustomerSearchMenu;
                case "2":
                return MenuTitle.LocationSearchMenu;
                case "3":
                return MenuTitle.ProductSearchMenu;
                case "4":
                return MenuTitle.CustomerOrderSearchMenu;
                case "5":
                return MenuTitle.StockOrderSearchMenu;
                default:
                return MenuTitle.Error;
            }
        }
    }
}