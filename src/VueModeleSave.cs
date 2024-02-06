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
        private String userInput;
        private ResourceManager rm;
        private Vue vueobject;
        private Sauvegardes saves;
        private String errorArgument = "Erreur CS8604 : argument invalide.";

        public VueModeleSave(String str)
        {
            this.vueobject = new Vue();
            this.saves = new Sauvegardes();
            this.rm = new ResourceManager("ConsoleApp1.languages." + str, Assembly.GetExecutingAssembly());
            Console.WriteLine(vueobject);
        }
        
        public void menu()
        {
            List<String> list = ["HOME_create_save", "HOME_lunch_save", "HOME_edit_save", "HOME_settings"];

            this.vueobject.SetOutPut(this.rm.GetString("HOME_hello")+"\n");
            this.vueobject.afficher();

            for (int i = 0; i <list.Count; i++)
            {
                this.vueobject.SetOutPut(i+1 + ") " + this.rm.GetString(list[i]));
                this.vueobject.afficher();
            }

            this.userInput = this.vueobject.GetInput();

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
                vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
            }
        }

        public void creerSauvegarde()
        { 
            vueobject.SetOutPut(rm.GetString("HOME_create_save") ?? errorArgument);
        }

        public void effectuerSauvegarde()
        {
            vueobject.SetOutPut(rm.GetString("HOME_lunch_save") ?? errorArgument);
        }

        public void modifierSauvegarde()
        {
            vueobject.SetOutPut(rm.GetString("HOME_edit_save") ?? errorArgument);
        }

        public void assignerParametres()
        {
            var lang = "en";

            ResourceManager RM = new ResourceManager("ConsoleApp1.languages." + lang, Assembly.GetExecutingAssembly());

            vueobject.SetOutPut(rm.GetString("SETTINGS_lang") ?? errorArgument);
        }

    }
}