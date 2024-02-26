using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour second_edit.xaml
    /// </summary>
    public partial class second_edit : Page
    {
        MainWindow m;
        public second_edit(MainWindow mainWindow)
        {
            InitializeComponent();
            this.m = m;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.m.afficher("edit");

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //TODO

        }

        public Object charger()
        {
            return this.Content;
        }

    }
}
