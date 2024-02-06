using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src
{
    internal class Vue
    {
        private String outPut;

        public Vue() {
            outPut = "";
        }

        public String GetInput()
        {
            String str = Console.ReadLine();
                return str;
        }

        public void SetOutPut(String str)
        {
            outPut = str;
        }

        public void afficher()
        {
            Console.WriteLine(this.outPut);
        }

    }
}
