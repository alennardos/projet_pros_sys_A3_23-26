using ConsoleApp1.languages;
using System.Resources;
using System.ComponentModel;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using ConsoleApp1.src.SaveType;
using Console_Application_Test_1.src;
using System.IO;
using System.Runtime.CompilerServices;

namespace ConsoleApp1.src;

class LangTest
{

    public static void Main(string[] args)
    {

        VueModeleSave vm = new VueModeleSave("fr");

        vm.menu();

        //ResourceManager rm = new ResourceManager("ConsoleApp1.languages.fr", Assembly.GetExecutingAssembly());

        //Console.WriteLine(rm.GetString("HOME_hello"));

        //Console.WriteLine(CallerFilePath);

        //var path = GetThisFilePath(); // path = @"path\to\your\source\code\file.cs"
        //var directory = Path.GetDirectoryName(path); // directory = @"path\to\your\source\code"

        //Console.WriteLine(path + "\\..\\..\\languages");
    }



}