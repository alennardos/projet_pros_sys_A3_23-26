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
    /// Logique d'interaction pour EditSave.xaml
    /// </summary>
    public partial class EditSave : Page
    {
        public EditSave()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            //back to home
            NavigationService?.Navigate(new MainWindow());
        }
    }
}
