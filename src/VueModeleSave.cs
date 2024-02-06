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
        //Sauvegardes saves;

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
                vueobject.SetOutPut("Erreur, veuillez rentrer un chiffre de 1 à 4 en fonction de vos besoins.");
            }
        }

        public void creerSauvegarde()
        {
            vueobject.SetOutPut("Veuillez entrer dans l'ordre en appuyant sur 'entrée' à chaque fois le nom, la source, la destination et le type.");
        }

        public void effectuerSauvegarde()
        {
            vueobject.SetOutPut("Taper 3-5 pour sauvegarder la 3 et 5 OU 3 à 5 pour sauvegarder la 3, 4 et 5.");
        }

        public void modifierSauvegarde()
        {
            vueobject.SetOutPut("Choisir le numéro de la sauvegarde allant de 1 à 5.");
        }

        public void assignerParametres()
        {
            vueobject.SetOutPut("Choissisez 1 pour mettre le logiciel en français ou 2 pour le mettre en anglais.");
        }

    }
}
