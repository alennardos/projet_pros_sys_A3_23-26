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
using System.Reflection.PortableExecutable;
using System.Data.SqlTypes;

namespace ConsoleApp1.src;

class LangTest
{
    public static void pain(string[] args)
    {
        // Créer l'objet de la vue modèle
        ViewModelSave vm = new ViewModelSave("fr", "xml");

        vm.menu();
    }
}