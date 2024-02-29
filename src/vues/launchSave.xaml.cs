using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SelectionMode = System.Windows.Controls.SelectionMode;
using static System.Collections.IList;
using static System.Windows.Forms.ListBox;
using System.Windows.Forms;
using System.IO;

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class LaunchSave : Page
    {

        MainWindow m;
        int size; // max size defined by user in settings
        int nbSavesMax; // Number of saves heavier than size
        List<string> listSrc = new List<string>(); // List of user's stocked saves sources

        public LaunchSave(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            loadLanguage();
        }

        private void loadLanguage()
        {
            launchSave.Content = m.GetResourceManager().GetString("LAUNCH_launch");
            Home.Content = m.GetResourceManager().GetString("home");
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.m.afficher("menu");
            this.nbSavesMax = 0;
            this.listSrc.Clear();
        }

        public void addSavesListeSave()
        {
            listeSaves.Items.Clear();
            ResourceManager rm = this.m.GetResourceManager();
            listeSaves.SelectionMode = SelectionMode.Multiple;

            foreach (Save s in m.GetSaves().getSaves())
            {
                listeSaves.Items.Add($"{rm.GetString("SAVE_name")}:     {s.GetName()},     {rm.GetString("SAVE_src")}: {s.GetSource()},     {rm.GetString("SAVE_dest")}: {s.GetDestination()}");
                this.listSrc.Add(s.GetSource());
            }
        }

        private void listeSaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.nbSavesMax = 0;
        }

        private void Launch_save_Click(object sender, RoutedEventArgs e)
        {
            List<int> index = new List<int>();

            List<List<int>> bigIndex = new List<List<int>>();

            this.nbSavesMax = 0;

            foreach (var item in listeSaves.SelectedItems)
            {
                // for loop -- used to determinate how many saves are heavier than max size 
                for (int i = 0; i < listSrc.Count; i++)
                {
                    if (Directory.Exists(listSrc[i]))
                    {
                        String currentItem = item.ToString();
                        String currentSource = listSrc[i].ToString();

                        int contains = currentItem.IndexOf(currentSource);

                        if (contains != -1)
                        {
                            long folderSizeBytes = CalculateFolderSize(listSrc[i]);
                            double folderSizeKB = folderSizeBytes / 1024.0;
                            int folderSizeKBrounded = (int)Math.Round(folderSizeKB);

                            if (folderSizeKBrounded > size)
                            {
                                this.nbSavesMax++;
                            }
                        }
                    }

                }
                index.Add(listeSaves.Items.IndexOf(item));
                
            }
            if (listeSaves.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show(m.GetResourceManager().GetString("error_nosave"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                this.m.makeSave(index);
                testlabel.Content = "";
            }
            catch (Exception ex)
            {
                string error_business_software = m.GetResourceManager().GetString("error_business_software");
                System.Windows.MessageBox.Show(""+ error_business_software +"\n"+ ex, "EasySave", MessageBoxButton.OK);
            }
            
        }

        public Object charger()
        {
            addSavesListeSave();
            this.size = m.getMaxSize();
            loadLanguage();
            return this.Content;
        }

        // Calculate folder size method
        private long CalculateFolderSize(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            long folderSize = 0;

            foreach (FileInfo file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                folderSize += file.Length;
            }

            return folderSize;
        }
    }
}
