using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src
{
    internal class ViewAppConsole
    {
        private String outPut;

        public ViewAppConsole() 
        {
            outPut = "";
        }

        // Get the user input
        public String GetInput()
        {
            String str = Console.ReadLine();

            return str;
        }

        // Define the user output
        public void SetOutPut(String str)
        {
            outPut = str;
        }

        // Show the output text
        public void show()
        {
            Console.WriteLine(this.outPut);
        }

    }
}
