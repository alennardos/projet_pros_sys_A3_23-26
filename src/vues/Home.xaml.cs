using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {

        MainWindow m;
        public Home(MainWindow m)
        {
            this.m = m;
            InitializeComponent();
            loadLanguage();
        }


        private void loadLanguage()
        {
            create.Content = m.GetResourceManager().GetString("HOME_create_save");
            lunch.Content = m.GetResourceManager().GetString("HOME_lunch_save");
            edit.Content = m.GetResourceManager().GetString("HOME_edit_save");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m.afficher("create");
        }

        private void lunch_Click(object sender, RoutedEventArgs e)
        {
            m.afficher("lunch");
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            m.afficher("edit");
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            m.afficher("settings");
        }

        public Object charger()
        {
            loadLanguage();
            return this.Content;
        }
    }
}
