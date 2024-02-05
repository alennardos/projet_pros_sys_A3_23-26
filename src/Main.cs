using ConsoleApp1.languages;
using System.Resources;
using System.ComponentModel;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using ConsoleApp1.src.SaveType;

namespace ConsoleApp1.src;

class LangTest
{

    public static void Main(string[] args)
    {

        Console.WriteLine("hello world");


        //var lang = "en";

        //ResourceManager RM = new ResourceManager("ConsoleApp1.languages." + lang, Assembly.GetExecutingAssembly());

        //Console.WriteLine(RM.GetString("options"));

        TypeSave ts = new SaveComplete();

        TypeSave ts2 = new SaveDif();

        Save s1 = new Save("s1", @"C:\CESI\A3\prog sys\git\projet_pros_sys_A3_23-26\test", @"C:\CESI\A3\prog sys\git\projet_pros_sys_A3_23-26\test2", ts);

        Console.WriteLine(s1.save());
        Console.ReadLine();
    }

}