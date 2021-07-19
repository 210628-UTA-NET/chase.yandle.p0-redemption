using System;

namespace UI
{
    public class AddMenu : IMenu
    {
        public void Menu()
        {
            Console.WriteLine("----Welcome to the adding menu!----");
            Console.WriteLine("[0] to return to the main menu");
            Console.WriteLine("[1] to add a customer");
            Console.WriteLine("[2] to add a store");
        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.MainMenu;
                case "1":
                return MenuTitle.CustomerAddMenu;
                case "2":
                return MenuTitle.LocationAddMenu;
                default:
                return MenuTitle.Error;
            }
        }
    }
}