using System;

namespace UI
{
    public class ProdSelect : IMenu
    {
        public void Menu()
        {
            Console.WriteLine("----Welcome to the Products menu!----");
            Console.WriteLine("[0] to return to the main menu");
            Console.WriteLine("[1] to search for a Game");
            Console.WriteLine("[2] to search for a Console");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.MainMenu;
                case "1":
                return MenuTitle.GameSearchMenu;
                case "2":
                return MenuTitle.SystemSearchMenu;
                default:
                return MenuTitle.Error;
            }
        }
    }
}