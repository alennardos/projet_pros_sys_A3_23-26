using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
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
using WpfApp1;

namespace ConsoleApp1
{
    public partial class SettingsPageMenu : Page
    {
        Saves saves;

        public SettingsPageMenu(MainWindow m)
        {

            InitializeComponent();
            this.m = m;

            combo_langages.Items.Add("xml");
            combo_langages.Items.Add("json");

            combo_langages.Items.Add("fr");
            combo_langages.Items.Add("en");

            this.saves = new Saves("a");

        }
        MainWindow m;

        private void combo_langages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceManager rm = m.GetResourceManager();
            ResourceManager tempo;

            if (combo_langages.SelectedIndex == 0)
            {
                tempo = new ResourceManager("WpfApp1.languages.fr", Assembly.GetExecutingAssembly());
                tempo.GetString("home");
                rm = tempo;
            }
            else if (combo_langages.SelectedIndex == 1)
            {
                tempo = new ResourceManager("WpfApp1.languages.en", Assembly.GetExecutingAssembly());
                tempo.GetString("home");
                rm = tempo;
            }
            else
            {
                //this.vueobject.SetOutPut(rm.GetString("home"));
                //this.vueobject.show();
            }

        }

        private void combo_typeLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceManager rm = m.GetResourceManager();

            if (combo_langages.SelectedIndex == 0)
            {
                saves.changeFormat("xml");
            }
            else if (combo_langages.SelectedIndex == 1)
            {
                saves.changeFormat("json");
            }
            else
            {
               // vueobject.SetOutPut(rm.GetString("home"));
               // vueobject.show();
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
