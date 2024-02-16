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
using Ookii.Dialogs.Wpf;

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour CreateSave2.xaml
    /// </summary>
    public partial class CreateSave : Page
    {

        MainWindow m;

        public CreateSave(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
        }

        private string OpenFolderDialog()
        {
            string selectedPath = "";
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                selectedPath = dialog.SelectedPath;
                // Utilisez le chemin sélectionné comme vous le souhaitez
            }
            return selectedPath;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            srcPath.Text = OpenFolderDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.m.afficher("menu");
        }
    }
}
