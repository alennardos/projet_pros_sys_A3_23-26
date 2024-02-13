using ConsoleApp1.src;
using ConsoleApp1.src.SaveType;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    internal class ViewModelSave
    {
        private String userInput;
        private String errorArgument = "Error CS8604 : invalid argument.";
        private bool run;
        private ResourceManager rm;
        private View vueobject;
        private Saves saves;

        public ViewModelSave(String str)
        {
            this.run = true;
            this.vueobject = new View();
            this.saves = new Saves();
            this.rm = new ResourceManager("ConsoleApp1.languages." + str, Assembly.GetExecutingAssembly());
            Console.WriteLine(vueobject);
        }

        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }
        //Enum use for the home menu
        enum menu_home
        { 
            createSave = 1,
            makeSave = 2,
            modifySave = 3,
            assignParameter = 4,
            quit = 5,

        }

        // Manage the home menu for the user and the input
        public void menu()
        {
            Console.Clear();

            List<String> list = ["HOME_create_save", "HOME_lunch_save", "HOME_edit_save", "HOME_settings", "quit"];

            this.vueobject.SetOutPut(this.rm.GetString("HOME_hello") + "\n");
            this.vueobject.show();

            while (this.run)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    this.vueobject.SetOutPut(i + 1 + ") " + this.rm.GetString(list[i]));
                    this.vueobject.show();
                }

                this.userInput = this.vueobject.GetInput();
                menu_home userInputEnum;

                if (Enum.TryParse(this.userInput, out userInputEnum))
                {
                    switch (userInputEnum)
                    {
                        case menu_home.createSave:
                            createSave();
                            break;

                        case menu_home.modifySave:
                            modifySave();
                            break;

                        case menu_home.makeSave:
                            makeSave();
                            break;

                        case menu_home.assignParameter:
                            assignParameter();
                            break;

                        case menu_home.quit:
                            this.run = false;
                            this.saves.quit();
                            break;

                        default:
                            vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                            vueobject.show();
                            break;
                    }
                }
                else
                {
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                    vueobject.show();
                }



                /*
                ======  Old code with old switch case =======

                this.userInput = this.vueobject.GetInput();

                switch (this.userInput)
                {
                    case "1":
                        createSave();
                        break;

                    case "3":
                        modifySave();
                        break;

                    case "2":
                        makeSave();
                        break;

                    case "4":
                        assignParameter();
                        break;

                    case "5":
                        this.run = false;
                        this.saves.quit();
                        break;

                    default:
                        vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                        vueobject.show();
                        break;
                   }
                    */
            }
            Console.Clear();
        }

        // Manage the save creation menu options for the user and the input
        public void createSave()
        {
            Console.Clear();
            vueobject.SetOutPut(rm.GetString("HOME_create_save") ?? errorArgument);
            List<String> list = ["CREATE_name_save", "CREATE_source_save", "CREATE_destination_save", "CREATE_type_save"];
            this.vueobject.SetOutPut(this.rm.GetString("home"));
            this.vueobject.show();

            string name = "";
            string source = "";
            string destination = "";
            TypeSave type = null;

            for (int i = 0; i < list.Count; i++)
            {
                this.vueobject.SetOutPut(i + 1 + ") " + this.rm.GetString(list[i]));
                this.vueobject.show();
                this.userInput = this.vueobject.GetInput();

                if (this.userInput == "")
                {
                    i--;
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                    vueobject.show();

                }
                
                switch (i)
                {
                    case 0:
                        name = this.userInput; break;

                    case 1:
                        source = this.userInput;break;
                    case 2:
                        destination = this.userInput; break;
                    case 3:
                        if (this.userInput == "1")
                            type = new SaveComplete();
                        else if (this.userInput == "2")
                            type = new SaveDif();
                        else
                        {
                            i--;
                            vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                            vueobject.show();
                        }
                        break;
                }
                
            }


            if (this.saves.createSave(name, source, destination, type))
            {
                Console.Clear();
                vueobject.SetOutPut(rm.GetString("CREATE_succes") + "\n");
                this.vueobject.show();
            }
            else
            {
                vueobject.SetOutPut(rm.GetString("CREATE_fail") + "\n");
                this.vueobject.show();
            }


        }

        //Verify if the process "name" is not active (use for block the lunch of a save if it is active)
        public bool processIsActive(string name)
        {
            Process[] localByName = Process.GetProcessesByName(name);
            return localByName.Length > 0;

        }

        // Manage the save making menu options for the user and the input
        public void makeSave()
        {
            if (processIsActive("Minecraft") == true)
            {
                Console.Clear();
                this.vueobject.SetOutPut(this.rm.GetString("LUNCH_cantrun"));
                this.vueobject.show();
                return;
            }

            Console.Clear();
            this.vueobject.SetOutPut(this.rm.GetString("LUNCH_info_save"));
            this.vueobject.show();
            
            this.vueobject.SetOutPut(this.rm.GetString("home"));
            this.vueobject.show();  
            int i = 1;

            foreach (Save s in  this.saves.getSaves())
            {
                this.vueobject.SetOutPut(i + ") " + s.GetName());
                this.vueobject.show();
                i++;
            }

            bool getInfo = false;

            List<int> aSauv = new List<int>();



            while (!getInfo)
            {
                try
                {
                    this.userInput = this.vueobject.GetInput();

                    if (this.userInput == "home")
                    {
                        Console.Clear();
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
                    else 
                    {
                        aSauv.Add(Int32.Parse(this.userInput)-1);
                    }

                    getInfo = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine (e.ToString());
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument + "\n");
                    vueobject.show();

                }

            }

            foreach(int j  in aSauv)
            {
                try
                {
                    this.saves.save(j);
                    Console.Clear();
                    vueobject.SetOutPut(j+1+" : "+rm.GetString("LUNCH_succes") ?? errorArgument+"\n");
                    vueobject.show();
                    } catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(" ");
                    vueobject.SetOutPut(rm.GetString("error_general") ?? errorArgument + "\n");
                    vueobject.show();
                }
            }

        }

        // Manage the save modifier menu options for the user and the input
        public void modifySave()
        {
            Console.Clear();
            vueobject.SetOutPut(rm.GetString("HOME_edit_save") ?? errorArgument);
            this.vueobject.show();

            this.vueobject.SetOutPut(this.rm.GetString("home"));
            this.vueobject.show();
            Console.WriteLine(" ");

            int i = 1;

            foreach (Save s in this.saves.getSaves())
            {

                this.vueobject.SetOutPut(i + ") " + s.GetName());
                this.vueobject.show();
                i++;
            }

            bool runEdit = true;

            while(runEdit)
            {
                this.userInput = this.vueobject.GetInput();

                if (this.userInput == "home") 
                {
                    Console.Clear();
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
                        this.vueobject.show();
                    }

                    this.userInput = this.vueobject.GetInput();

                    switch (this.userInput)
                    {
                        case "home":
                            Console.Clear();
                            return;

                        case "1":
                            s.SetName(this.vueobject.GetInput());
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.show();
                            break;

                        case "2":
                            s.SetSource(this.vueobject.GetInput());
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.show();
                            break;

                        case "3":
                            s.SetDestination(this.vueobject.GetInput());
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.show();
                            break;

                        case "4":
                            bool case4 = true;
                            while (case4)
                            {
                                this.vueobject.SetOutPut(this.rm.GetString("CREATE_type_save"));
                                this.vueobject.show();
                                this.userInput = vueobject.GetInput();
                                case4 = false;
                                if (this.userInput == "1")
                                    s.setTs(new SaveComplete());
                                else if (this.userInput == "2")
                                    s.setTs(new SaveDif());
                                else
                                {
                                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                                    vueobject.show();
                                    case4 = true;
                                }
                            }
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.show();
                            break;

                        case "5":
                            Console.Clear();
                            this.saves.removeSave(index-1);
                            vueobject.SetOutPut(rm.GetString("EDIT_succes") + "\n");
                            this.vueobject.show();
                            break;

                        default:
                            Console.Clear();
                            vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                            vueobject.show();
                            break;
                    }


                }
                catch (Exception e) 
                {
                    vueobject.SetOutPut(rm.GetString("enter_bad") ?? errorArgument);
                    vueobject.show();
                    runEdit = true;
                }

            }

            Console.Clear();
        }

        // Manage the parameter assigner menu options for the user and the input
        public void assignParameter()
        {
            Console.Clear();
            vueobject.SetOutPut(rm.GetString("SETTINGS_lang") ?? errorArgument);
            vueobject.show();

            var path = GetThisFilePath();

            DirectoryInfo dirLang = new DirectoryInfo(path + "\\..\\..\\languages");

            bool skip = false;
            int i = 1;

            foreach(FileInfo file in dirLang.GetFiles())
            {
                if (skip)
                {
                    vueobject.SetOutPut("- "+file.Name.Split('.')[0]);
                    vueobject.show();
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
                    vueobject.show();
                }
                
            }
            Console.Clear();
        }

    }
}