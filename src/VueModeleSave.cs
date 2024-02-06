using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application_Test_1.src
{
    internal class VueModeleSave
    {
        String userInput;
        ResourceManager rm;
        Vue vueobject;
        Sauvegardes saves;
        String errorArgument = "Erreur CS8604 : argument invalide.";

        public VueModeleSave(String str)
        {
            new ResourceManager("ConsoleApp1.languages." + str, Assembly.GetExecutingAssembly());
        }
          
        public void menu()
        {
            this.userInput = Vue.GetInput();

            if (this.userInput.Equals("1"))
            {
                creerSauvegarde();
            }
            else if (this.userInput.Equals("2"))
            {
                modifierSauvegarde();
            }
            else if (this.userInput.Equals("3"))
            {
                effectuerSauvegarde();
            }
            else if(this.userInput.Equals("4"))
            {
                assignerParametres();
            }
            else
            {
                vueobject.SetOutPut(rm.GetString("entrer_bad") ?? errorArgument);
            }
        }

        public void creerSauvegarde()
        {
            vueobject.SetOutPut(rm.GetString("create_save") ?? errorArgument);
        }

        public void effectuerSauvegarde()
        {
            vueobject.SetOutPut(rm.GetString("lunch_save") ?? errorArgument);
        }

        public void modifierSauvegarde()
        {
            vueobject.SetOutPut(rm.GetString("edit_save") ?? errorArgument);
        }

        public void assignerParametres()
        {
            var lang = "en";

            ResourceManager RM = new ResourceManager("ConsoleApp1.languages." + lang, Assembly.GetExecutingAssembly());

            vueobject.SetOutPut(rm.GetString("settings") ?? errorArgument);
        }

    }
}