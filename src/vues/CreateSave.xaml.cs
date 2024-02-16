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
    /// Logique d'interaction pour CreateSave2.xaml
    /// </summary>
    public partial class CreateSave : Page
    {

        MainWindow main;

        public CreateSave(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MainWindow();
        }
    }
}
