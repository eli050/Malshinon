using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Controlers;

namespace Malshinon.Menus
{
    public  class Menu
    {
        public static void SelectionMenu()
        {
            bool NotExit = true;
            Console.WriteLine("Welcome to the system.");
            while (NotExit)
            {
                Console.WriteLine(
                " please enter your selection:" +
                "\na. Create report" +
                "\nb. Logout");
                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "a":
                        Management.Control();
                        break;
                    case "b":
                        NotExit = false;
                        break;
                }
            }
            

        }
    }
}
