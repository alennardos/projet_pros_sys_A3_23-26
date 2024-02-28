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

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class LaunchSave : Page
    {

        MainWindow m;

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
        }

        public void addSavesListeSave()
        {
            listeSaves.Items.Clear();
            ResourceManager rm = this.m.GetResourceManager();
            listeSaves.SelectionMode = SelectionMode.Multiple;

            foreach (Save s in m.GetSaves().getSaves())
            {
                listeSaves.Items.Add($"{rm.GetString("SAVE_name")}:     {s.GetName()},     {rm.GetString("SAVE_src")}: {s.GetSource()},     {rm.GetString("SAVE_dest")}: {s.GetDestination()}");
            }
        }

        private void listeSaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Launch_save_Click(object sender, RoutedEventArgs e)
        {
            List<int> index = new List<int>();
            foreach(var item in listeSaves.SelectedItems)
            {
                index.Add(listeSaves.Items.IndexOf(item));
                
            }
            try
            {
                this.m.makeSave(index);
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
            loadLanguage();
            return this.Content;
        }
    }
}
