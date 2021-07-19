using System;
using System.Threading;

namespace UI
{
    public class SplashScreen
    {
        public static void SplashMessage()
        {
            Console.Clear();
            Console.WriteLine(@"  #####     #    #     # #######  #####  #     # ####### ######  ");
            Console.WriteLine(@" #     #   # #   ##   ## #       #     # #     # #     # #     # ");
            Console.WriteLine(@" #        #   #  # # # # #       #       #     # #     # #     # ");
            Console.WriteLine(@" #  #### #     # #  #  # #####    #####  ####### #     # ######  ");
            Console.WriteLine(@" #     # ####### #     # #             # #     # #     # #       ");
            Console.WriteLine(@" #     # #     # #     # #       #     # #     # #     # #       ");
            Console.WriteLine(@"  #####  #     # #     # #######  #####  #     # ####### #       ");
            Thread.Sleep(2000);
        }
    }
}