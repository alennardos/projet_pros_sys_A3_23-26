using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src
{
    internal class Vue
    {
        private string? outPut;

        public static string GetInput()
        {
            string str = Console.ReadLine();
                return str;
        }

        public void SetOutPut(string str)
        {
            outPut = str;
        }

        public void afficher()
        {
            Console.WriteLine(this.outPut);
        }

    }
}
