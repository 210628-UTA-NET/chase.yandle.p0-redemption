using System;

namespace UI
{
    public class MainMenu : IMenu
    {
        public void Menu()
        {
            Console.WriteLine("----Please select the menu you would like to go to----");
            Console.WriteLine("[0] to exit program");
            Console.WriteLine("[1] to log out");
            Console.WriteLine("[2] to add customers or stores");
            Console.WriteLine("[3] to search (Products / Orders) or search and edit (customers / stores)");
            Console.WriteLine("[4] to place a new order");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.Exit;
                case "1":
                return MenuTitle.LoginMenu;
                case "2":
                return MenuTitle.AddMenu;
                case "3":
                return MenuTitle.SearchMenu;
                case "4":
                return MenuTitle.OrderMenu;
                default:
                return MenuTitle.Error;
            }
        }
    }
}