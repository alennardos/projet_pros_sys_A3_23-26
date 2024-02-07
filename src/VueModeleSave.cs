using ConsoleApp1.src;
using ConsoleApp1.src.SaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Application_Test_1.src
{
    internal class VueModeleSave
    {
        private String userInput;
        private ResourceManager rm;
        private Vue vueobject;
        private Sauvegardes saves;
        private String errorArgument = "Erreur CS8604 : argument invalide.";
        private bool run;

        

        public VueModeleSave(String str)
        {
            this.run = true;
            this.vueobject = new Vue();
            this.saves = new Sauvegardes();
            this.rm = new ResourceManager("ConsoleApp1.languages." + str, Assembly.GetExecutingAssembly());
            Console.WriteLine(vueobject);
        }

        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        public void menu()
        {

            List<String> list = ["HOME_create_save", "HOME_lunch_save", "HOME_edit_save", "HOME_settings", "quit"];

            this.vueobject.SetOutPut(this.rm.GetString("HOME_hello") + "\n");
            this.vueobject.afficher();


            while (this.run)
            {

            for (int i = 0; i < list.Count; i++)
            {
                this.vueobject.SetOutPut(i + 1 + ") " + this.rm.GetString(list[i]));
                this.vueobject.afficher();
            }

                this.userInput = this.vueobject.GetInput();


                switch (this.userInput)
                {
                    case "1":
                        creerSauvegarde();
                        break;

                    case "3":
                        modifierSauvegarde();
                        break;

                    case "2":
                        effectuerSauvegarde();
                        break;

                    case "4":
                        assignerParametres();
                        break;

                    case "5":
                        this.run = false;
                        this.saves.quit();
                        break;

                    default:
                        vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                        vueobject.afficher();
                        break;
                }
            }
            
        }

        public void creerSauvegarde()
        {
            
            vueobject.SetOutPut(rm.GetString("HOME_create_save") ?? errorArgument);
            List<String> list = ["CREATE_name_save", "CREATE_source_save", "CREATE_destination_save", "CREATE_type_save"];
            this.vueobject.SetOutPut(this.rm.GetString("home"));
            this.vueobject.afficher();

            string name = "";
            string src = "";
            string dst = "";
            TypeSave type = null;
            

            for (int i = 0; i < list.Count; i++)
            {
                this.vueobject.SetOutPut(i + 1 + ") " + this.rm.GetString(list[i]));
                this.vueobject.afficher();
                this.userInput = this.vueobject.GetInput();

                if (this.userInput == "")
                {
                    i--;
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                    vueobject.afficher();

                }
                
                switch (i)
                {
                    case 0:
                        name = this.userInput; break;

                    case 1:
                        src = this.userInput;break;
                    case 2:
                        dst = this.userInput; break;
                    case 3:
                        if (this.userInput == "1")
                            type = new SaveComplete();
                        else if (this.userInput == "2")
                            type = new SaveDif();
                        else
                        {
                            i--;
                            vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                            vueobject.afficher();
                        }
                        break;
                }
                
            }


            if (this.saves.createSave(name, src, dst, type))
            {
                vueobject.SetOutPut(rm.GetString("CREATE_succes") + "\n");
                this.vueobject.afficher();
            }
            else
            {
                vueobject.SetOutPut(rm.GetString("CREATE_fail") + "\n");
                this.vueobject.afficher();
            }


        }

        public void effectuerSauvegarde()
        {
            this.vueobject.SetOutPut(this.rm.GetString("LUNCH_info_save"));
            this.vueobject.afficher();
            
            this.vueobject.SetOutPut(this.rm.GetString("home"));
            this.vueobject.afficher();  
            int i = 1;

            foreach (Save s in  this.saves.getSaves())
            {
                this.vueobject.SetOutPut(i + ") " + s.GetName());
                this.vueobject.afficher();
                i++;
            }
            bool infoRecup = false;

            List<int> aSauv = new List<int>();

            while (!infoRecup)
            {
                try
                {
                    this.userInput = this.vueobject.GetInput();

                    if (this.userInput == "home")
                    {
                        return;
                    }

                    if (this.userInput.Contains(";"))
                    {
                        foreach (string saveIndex in this.userInput.Split(";"))
                        {
                            aSauv.Add(Int32.Parse(saveIndex)-1);
                        }
                    }
                    else if (this.userInput.Contains("-"))
                    {
                        
                        for (int j = Int32.Parse(this.userInput.Split("-")[0]); j <= Int32.Parse(this.userInput.Split("-")[1]); j++)
                        {
                            aSauv.Add(j-1);
                        }
                    }
                    else {
                        Console.WriteLine("val : "+Int32.Parse(this.userInput));
                        aSauv.Add(Int32.Parse(this.userInput)-1);
                    }
                    infoRecup = true;
                }catch (Exception e){
                    Console.WriteLine (e.ToString());
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument + "\n");
                    vueobject.afficher();

                }

            }

            foreach(int j  in aSauv)
            {
                try
                {
                    this.saves.save(j);
                    vueobject.SetOutPut(j+1+" : "+rm.GetString("LUNCH_succes") ?? errorArgument+"\n");
                    vueobject.afficher();
                    } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    vueobject.SetOutPut(rm.GetString("error_general") ?? errorArgument + "\n");
                    vueobject.afficher();
                }
            }
        }

        public void modifierSauvegarde()
        {
            vueobject.SetOutPut(rm.GetString("HOME_edit_save") ?? errorArgument);
            this.vueobject.afficher();

            this.vueobject.SetOutPut(this.rm.GetString("home"));
            this.vueobject.afficher();
            Console.WriteLine(" ");

            int i = 1;

            foreach (Save s in this.saves.getSaves())
            {

                this.vueobject.SetOutPut(i + ") " + s.GetName());
                this.vueobject.afficher();
                i++;
            }

            bool runEdit = true;

            while(runEdit)
            {
                this.userInput = this.vueobject.GetInput();

                if (this.userInput == "home") 
                {
                    return;
                }

                try
                {
                    runEdit = false;
                    int index = Int32.Parse(this.userInput);
                    Save s = this.saves.getSaves()[index-1];

                    List<String> list = ["EDIT_name_save", "EDIT_source_save", "EDIT_destination_save", "EDIT_save_type", "EDIT_delet_save"];

                    for (i = 0; i < list.Count; i++)
                    {
                        this.vueobject.SetOutPut(i + 1 + ") " + this.rm.GetString(list[i]));
                        this.vueobject.afficher();
                    }

                    this.userInput = this.vueobject.GetInput();

                    switch (this.userInput)
                    {
                        case "home":
                            return;

                        case "1":
                            s.SetName(this.vueobject.GetInput());
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.afficher();
                            break;

                        case "2":
                            s.SetSource(this.vueobject.GetInput());
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.afficher();
                            break;

                        case "3":
                            s.SetDest(this.vueobject.GetInput());
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.afficher();
                            break;

                        case "4":
                            bool case4 = true;
                            while (case4)
                            {
                                this.vueobject.SetOutPut(this.rm.GetString("CREATE_type_save"));
                                this.vueobject.afficher();
                                this.userInput = vueobject.GetInput();
                                case4 = false;
                                if (this.userInput == "1")
                                    s.setTs(new SaveComplete());
                                else if (this.userInput == "2")
                                    s.setTs(new SaveDif());
                                else
                                {
                                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                                    vueobject.afficher();
                                    case4 = true;
                                }
                            }
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.afficher();
                            break;

                        case "5":
                            this.saves.removeSave(index-1);
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.afficher();
                            break;

                        default:
                            vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                            vueobject.afficher();
                            break;
                    }


                }
                catch (Exception e) 
                {
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                    vueobject.afficher();
                    runEdit = true;
                }

            }

        }

        public void assignerParametres()
        {

            vueobject.SetOutPut(rm.GetString("SETTINGS_lang") ?? errorArgument);
            vueobject.afficher();

            var path = GetThisFilePath();

            DirectoryInfo dirLang = new DirectoryInfo(path + "\\..\\..\\languages");

            bool skip = false;
            int i = 1;

            foreach(FileInfo file in dirLang.GetFiles())
            {
                if (skip)
                {
                    vueobject.SetOutPut("- "+file.Name.Split('.')[0]);
                    vueobject.afficher();
                    i++;
                }
                skip = !skip;
            }

            bool ok = false;

            while (!ok)
            {
                try
                {
                    ResourceManager tempo = new ResourceManager("ConsoleApp1.languages." + vueobject.GetInput(), Assembly.GetExecutingAssembly());
                    tempo.GetString("home");
                    this.rm = tempo;
                    ok = true;
                }catch
                {
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                    vueobject.afficher();
                }
                
            }

        }

    }
}