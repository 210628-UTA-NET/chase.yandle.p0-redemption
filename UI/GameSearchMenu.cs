using System;
using Models;
using System.Collections.Generic;
using BL;
using DL;
using System.Threading;
using System.Globalization;

namespace UI
{
    public class GameSearchMenu : IMenu
    {
        public static Games result = new Games();
        private Games filter = new Games();
        public static bool forOrder = false;
        List<Games> readout = new List<Games>();
        private GamesBL _gamesBL;
        public bool noFilter = true;
        public GameSearchMenu(GamesBL p_gamesBL)
        {
            _gamesBL=p_gamesBL;
        }
        public void Menu()
        {
            if (noFilter)
            {
                readout=_gamesBL.GetAllGames();
                Console.WriteLine("----Game Search Menu----");
                Console.WriteLine("[0] to return to the product selection menu");
                Console.WriteLine("[1] to retrieve all games ("+readout.Count+")");
                Console.WriteLine("[2] to apply search filters");
            } else
            {
                Console.WriteLine("----Game Search Menu----");
                Console.WriteLine("[0] to return to the product selection menu");
                Console.WriteLine("[1] to retrieve filtered list of games ("+readout.Count+")");
                Console.WriteLine("[2] to apply more search filters");
                Console.WriteLine("[3] to clear all search filters");
            }

        }
        public MenuTitle UInput()
        {
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "0":
                return MenuTitle.ProductSearchMenu;
                case "1":
                GameList(readout);
                return MenuTitle.GameSearchMenu;
                case "2":
                FilterGames();
                noFilter=false;
                return MenuTitle.GameSearchMenu;
                case "3":
                if (noFilter)
                {
                    return MenuTitle.Error;
                } else
                {
                    noFilter=true;
                    return MenuTitle.GameSearchMenu;
                }
                default:
                return MenuTitle.Error;
            }
        }
        public void GameList(List<Games> reading)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Game List");
            foreach (Games game in reading)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine(i+".  Name: "+game.gName +" | System: "+ game.gSystem +" | MSRP: "+ game.gMSRP);
                Console.WriteLine("=======================================");
                i++;
            }
            Console.WriteLine("End of list");
            Console.WriteLine("Enter 0 to return to search menu");
            if (forOrder)
            {
                Console.WriteLine("Enter the number of the game that is being placed this order");
            }
            int choice = int.Parse(Console.ReadLine());
            if (choice==0)
            {
                Console.WriteLine("Returning to game search menu");
            } else if (choice<=reading.Count)
            {
                if (forOrder)
                {
                    result = reading[(choice-1)];
                }
                
            } else
            {
                Console.WriteLine("That input was not valid, please try again!");
            }
        }
    private void FilterGames()
    {
        bool loop = true;
            while (loop)
            {
            Console.WriteLine("----Game Filter Interface----");
            Console.WriteLine("Game: "+filter.gName);
            Console.WriteLine("---------------------------");
            Console.WriteLine("[0] to remove filters and return to game search menu");
            Console.WriteLine("[1] to use the selected filters");
            Console.WriteLine("[2] to Search for a game by name");
            string choice = Console.ReadLine();
            Console.WriteLine("--------------------");
            switch (choice)
            {
                case "0":
                    noFilter=true;
                    loop=false;
                    filter=new Games();
                    break;
                case "1":
                    loop=false;
                    readout=_gamesBL.SearchGame(filter.gName);
                    Console.WriteLine("Filter Applied!  Press Enter to Continue");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Game:");
                    filter.gName=Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("That input was not valid, please try again!");
                    break;
                }          
            }
        }
    }
}
