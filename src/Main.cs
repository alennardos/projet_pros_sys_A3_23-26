using ConsoleApp1.languages;
using System.Resources;
using System.ComponentModel;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using ConsoleApp1.src.SaveType;
using Console_Application_Test_1.src;
using System.IO;
using System.Runtime.CompilerServices;
using System;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1.src;

class LangTest
{

    public static void Main(string[] args)
    {
        // Créer l'objet de la vue modèle
        VueModeleSave vm = new VueModeleSave("fr");

        // Lance le menu à l'exécution
        vm.menu();

        //XElement purchaseOrder = XElement.Load(@"C:\CESI\A3\prog sys\projet\projet_pros_sys_A3_23-26\save\save.xml");



        //saveFile

        //Console.WriteLine(purchaseOrder.Value);

    }



}