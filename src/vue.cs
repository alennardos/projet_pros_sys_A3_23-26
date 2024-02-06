using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src
{
    internal class Vue
    {
        private string outPut;

        private static string GetInput()
        {
            string str = Console.ReadLine();
            if (str == null || str.Length == 0)
            {
                //Console.WriteLine("");
                return "";
            }
            else
                return str;
        }

        private void SetOutPut()
        {

        }

        private void afficher()
        {
            Console.WriteLine("");
        }

    }
}
