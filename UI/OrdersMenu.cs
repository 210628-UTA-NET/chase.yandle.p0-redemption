using System;

namespace UI
{
    public class OrderMenu : IMenu
    {
        public void Menu()
        {
            Console.WriteLine("----Welcome to the order menu!----");
            Console.WriteLine("[0] to return to the main menu");
            Console.WriteLine("[1] to place a customer order");
            Console.WriteLine("[2] to place a stock order");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.MainMenu;
                case "1":
                return MenuTitle.CustomerOrderMenu;
                case "2":
                return MenuTitle.StockOrderMenu;
                default:
                return MenuTitle.Error;
            }
        }
    }
}