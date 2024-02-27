using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            loadLanguage();
        }

        private void loadLanguage()
        {
            saveNameLabel.Content = m.GetResourceManager().GetString("CREATE_name_save");
            selectPathSrc.Content = m.GetResourceManager().GetString("CREATE_source_save");
            selectPathDst.Content = m.GetResourceManager().GetString("CREATE_destination_save");
            SaveComplete.Content = m.GetResourceManager().GetString("TYPE_complete");
            SaveDiff.Content = m.GetResourceManager().GetString("TYPE_diff");
            create.Content = m.GetResourceManager().GetString("CREATE_create");
            home.Content = m.GetResourceManager().GetString("home");

            label_selectPathDest.Content = m.GetResourceManager().GetString("CREATE_destination_instructions");
            label_selectPathSource.Content = m.GetResourceManager().GetString("CREATE_source_instructions");
            label_selectSaveType.Content = m.GetResourceManager().GetString("CREATE_save_type_instructions");
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

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.m.afficher("menu");
        }

        private void selectSrc_Click(object sender, RoutedEventArgs e)
        {
            srcPath.Text = OpenFolderDialog();
        }

        private void selectDest_Click(object sender, RoutedEventArgs e)
        {
            dstPath.Text = OpenFolderDialog();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            bool good = true;

            if (saveName.Text.Equals(""))
            { 
                saveName.BorderBrush = System.Windows.Media.Brushes.Red;
                good = false;
            }
            else
            {
                saveName.BorderBrush = null;
            }

            if (dstPath.Text.Equals(""))
            {
                dstPath.BorderBrush = System.Windows.Media.Brushes.Red;
                good = false;

            }
            else
            {
                dstPath.BorderBrush = null;
            }

            if (srcPath.Text.Equals(""))
            {
                srcPath.BorderBrush = System.Windows.Media.Brushes.Red;
                good = false;
            }
            else
            {
                srcPath.BorderBrush = null;
            }

            if (good == true)
            {
                this.m.createSave(saveName.Text, srcPath.Text, dstPath.Text, "complete");

                srcPath.Text = "";
                dstPath.Text = "";
                saveName.Text = "";

                SaveComplete.IsChecked = true;
                SaveDiff.IsChecked = false;

                HomeButton_Click(this, new RoutedEventArgs());
            }
        }

        public Object charger()
        {
            loadLanguage();
            return this.Content;
        }

        private void saveName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveDiff_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SaveComplete_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}