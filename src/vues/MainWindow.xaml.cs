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
using ProgressBarVue = WpfApp1.src.vues.ProgressBarVue;
using System.Net.Sockets;
using System.Net;

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
        LaunchSave vueLaunch;
        SettingsPageMenu vueSettings;
        second_edit vueSecondEdit;

        private Saves saves;
        private ResourceManager rm;
        private Save saveModif;
        private int size = 0;

        Socket s;

        //Settings_menu vueSettings;
        public MainWindow()
        {
            //single instance case
            if (processIsActive("EasySave"))
            {
                System.Windows.MessageBox.Show(rm.GetString("error_running"));
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                InitializeComponent();

                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                s.Bind(ipep);
                s.Listen();

                this.saves = Saves.Instance();
                this.rm = new ResourceManager("WpfApp1.languages.fr", Assembly.GetExecutingAssembly());

                vueSettings = new SettingsPageMenu(this);
                vueSave = new CreateSave(this);
                vueHome = new Home(this);
                this.Content = vueHome;
                vueEdit = new EditSave(this);
                vueLaunch = new LaunchSave(this);
                vueSecondEdit = new second_edit(this);

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            Width = screenWidth;
            Height = screenHeight;
        }

        public static void save(Object save)
        {
            ((Saves)((List<Object>)save).ElementAt(0)).save((int)((List<Object>)save).ElementAt(1));
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
                case "launch":
                    this.Content = vueLaunch.charger();
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

                save.Start(new List<object>() {this.saves, index});

                ProgressBarVue pb = new ProgressBarVue(this.saves.getSaves()[index], s, rm);

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

        public void setMaxSize(int size)
        {
            this.size = size;
        }
        public int getMaxSize()
        {
            return this.size;
        }

        public Save getSaveModif()
        {
            return this.saveModif;
        }

    }
}