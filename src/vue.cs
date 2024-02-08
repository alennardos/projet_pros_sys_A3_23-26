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

        // Récupere l'entrée utilisateur
        public String GetInput()
        {
            String str = Console.ReadLine();
                return str;
        }

        // Définit la sortie utilisateur
        public void SetOutPut(String str)
        {
            outPut = str;
        }

        // Affiche le texte de sortie
        public void afficher()
        {
            Console.WriteLine(this.outPut);
        }

    }
}
