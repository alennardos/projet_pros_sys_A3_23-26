using ConsoleApp1.languages;
using System.Resources;
using System.ComponentModel;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using ConsoleApp1.src.SaveType;
using Console_Application_Test_1.src;

namespace ConsoleApp1.src;

class LangTest
{

    public static void Main(string[] args)
    {

        VueModeleSave vm = new VueModeleSave("fr");

        vm.menu();

        //ResourceManager rm = new ResourceManager("ConsoleApp1.languages.fr", Assembly.GetExecutingAssembly());

        //Console.WriteLine(rm.GetString("HOME_hello"));
    }

}