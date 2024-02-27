using ConsoleApp1.src;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using WpfApp1.src.vues;
using static System.Windows.Forms.DataFormats;
using System;
using ConsoleApp1.src.SaveType;
using System.Diagnostics;
using System.Collections;
using ConsoleApp1;
using System.Linq.Expressions;
using ProgressBar = WpfApp1.src.vues.ProgressBar;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CreateSave vueSave;
        Home vueHome;
        EditSave vueEdit;
        LunchSave vueLunch;
        SettingsPageMenu vueSettings;
        second_edit vueSecondEdit;

        private Saves saves;
        private ResourceManager rm;
        private bool run;
        private Save saveModif;

        //Settings_menu vueSettings;
        public MainWindow()
        {
            InitializeComponent();

            this.run = true;
            this.saves = Saves.Instance();
            this.rm = new ResourceManager("WpfApp1.languages.fr", Assembly.GetExecutingAssembly());

            vueSettings = new SettingsPageMenu(this);
            Thread.Sleep(100);
            vueSave = new CreateSave(this);
            vueHome = new Home(this);
            this.Content = vueHome;
            vueEdit = new EditSave(this);
            vueLunch = new LunchSave(this);
            vueSecondEdit = new second_edit(this);

        }

        public static void save(Object save)
        {
            ((Save)save).save();
        }

        public static void saveProgress(Object save)
        {
            //ProgressBar pb = new ProgressBar((Save)save);
            //pb.Show();

        }

        public void afficher(string page)
        {
            switch (page)
            {
                case "menu":
                    this.Content = vueHome.charger();
                    break;
                case "create":
                    this.Content = vueSave.charger();
                    break;
                case "edit":
                    this.Content = vueEdit.charger();

                    break;
                case "lunch":
                    this.Content = vueLunch.charger();
                    break;
                case "settings":
                    this.Content = vueSettings.charger();
                    break;
                case "secondEdit":
                    this.Content = vueSecondEdit.charger();
                    break;
            }
        }
        public Saves GetSaves()
        {
            return this.saves;
        }

        public ResourceManager GetResourceManager()
        {
            return this.rm;
        }

        public void createSave(String saveName, String pathSrc, String pathDest, String type)
        {
            TypeSave ts = null;
            if (type == "complete")
            {
                ts = new SaveComplete();
            }
            else
            {
                ts = new SaveDif();
            }
            this.saves.createSave(saveName, pathSrc, pathDest, ts);
        }

        public bool processIsActive(string name)
        {
            Process[] localByName = Process.GetProcessesByName(name);
            return localByName.Length > 0;

        }
        // A
        public void makeSave(List<int> savesIndex)
        {
            string software = "Minecraft";
            if (processIsActive(software) == true)
            {
                throw new Exception("The business software " + software + " is active \n Please close "+software);
            }

            foreach (int index in savesIndex)
            {
                Thread save = new Thread(MainWindow.save);
                save.Start(this.saves.getSaves()[index]);
                ProgressBar pb = new ProgressBar(this.saves.getSaves()[index]);
                pb.Show();
                //Thread saveProgress = new Thread(MainWindow.saveProgress);
                //saveProgress.SetApartmentState(ApartmentState.STA);
                //saveProgress.Start(this.saves.getSaves()[index]);

            }
        }

        public void changeLanguage(string langue)
        {
            try
            {
                rm = new ResourceManager("WpfApp1.languages." + langue, Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                
            }
        }
        public void changeLogTypes(string type)
        {
            try
            {
                saves.changeFormat(type);
            }
            catch (Exception e)
            {

            }
        }

        public void changeCrypt(bool crypt)
        {
            try
            {
                saves.changeCrypt(crypt);
            }
            catch (Exception e)
            {

            }
        }

        public void setSaveModif(int index)
        {
            this.saveModif = this.saves.getSaves()[index];
        }
        public Save getSaveModif()
        {
            return this.saveModif;
        }

    }
}