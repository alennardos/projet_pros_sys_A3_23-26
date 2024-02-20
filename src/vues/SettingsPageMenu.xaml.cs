using ConsoleApp1.src;
using System;
using System.Collections.Generic;
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

namespace ConsoleApp1
{
    public partial class SettingsPageMenu : Page
    {
        private Saves saves;
        private ViewAppConsole console;
        private ResourceManager rm;

        public SettingsPageMenu()
        {
            InitializeComponent();

            combo_langages.Items.Add("xml");
            combo_langages.Items.Add("json");

            combo_langages.Items.Add("fr");
            combo_langages.Items.Add("en");
        }

        private void combo_langages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceManager tempo;


            if (combo_langages.SelectedIndex == 0)
            {
                tempo = new ResourceManager("WpfApp1.languages.fr", Assembly.GetExecutingAssembly());
                tempo.GetString("home");
                this.rm = tempo;
            }
            else if (combo_langages.SelectedIndex == 1)
            {
                tempo = new ResourceManager("WpfApp1.languages.en", Assembly.GetExecutingAssembly());
                tempo.GetString("home");
                this.rm = tempo;
            }
            else
            {
                this.console.SetOutPut(this.rm.GetString("home"));
                this.console.show();
            }

        }

        private void combo_typeLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo_langages.SelectedIndex == 1)
            {
                saves.changeFormat("xml");
            }
            else if (combo_langages.SelectedIndex == 2)
            {
                saves.changeFormat("json");
            }
            else
            {
                this.console.SetOutPut(this.rm.GetString("home"));
                this.console.show();
            }
        }
    }
}
