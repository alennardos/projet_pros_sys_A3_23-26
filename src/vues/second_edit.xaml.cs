using ConsoleApp1.src;
using ConsoleApp1.src.SaveType;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
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
        Save saveModif;
       
        MainWindow m;

        public second_edit(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            loadLanguage();
        }

        private void loadLanguage()
        {
            
            saveNameLabel.Content = m.GetResourceManager().GetString("SAVE_name");
            selectPathSourceLabel.Content = m.GetResourceManager().GetString("SAVE_src");
            selectPathSrc.Content = m.GetResourceManager().GetString("FILE_choose");
            selectPathDestLabel.Content = m.GetResourceManager().GetString("SAVE_dest");
            selectPathDest.Content = m.GetResourceManager().GetString("FILE_choose");
            SelectSaveTypeLabel.Content = m.GetResourceManager().GetString("CREATE_type_save");
            SaveComplete.Content = m.GetResourceManager().GetString("TYPE_complete");
            SaveDiff.Content = m.GetResourceManager().GetString("TYPE_diff");
            saveButton.Content = m.GetResourceManager().GetString("save");
            cancelButton.Content = m.GetResourceManager().GetString("cancel");
            removeButton.Content = m.GetResourceManager().GetString("EDIT_delet_save");
            
            


        }
        private string OpenFolderDialog()
        {
            string selectedPath = "";
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                selectedPath = dialog.SelectedPath;
            }
            return selectedPath;
        }

        public void setSaveModif()
        {
            this.saveModif = m.getSaveModif();
            saveName.Text = this.saveModif.GetName();
            saveSrcPath.Text = this.saveModif.GetSource();
            saveDestPath.Text = this.saveModif.GetDestination();
           
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.m.afficher("edit");
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            bool good = true;

            if (saveName.Text.Equals(""))
            {
                saveName.BorderBrush = System.Windows.Media.Brushes.Red;
                good = false;
            }
            else
            {
                saveName.BorderBrush = default;
            }

            if (saveDestPath.Text.Equals(""))
            {
                saveDestPath.BorderBrush = System.Windows.Media.Brushes.Red;
                good = false;

            }
            else
            {
                saveDestPath.BorderBrush = default;
            }

            if (saveSrcPath.Text.Equals(""))
            {
                saveSrcPath.BorderBrush = System.Windows.Media.Brushes.Red;
                good = false;
            }
            else
            {
                saveSrcPath.BorderBrush = default;
            }
            if (good)
            {
                if (SaveDiff.IsChecked == true)
                    saveModif.setTs(new SaveDif());
                if(SaveComplete.IsChecked == true)
                    saveModif.setTs(new SaveComplete());

                
                    saveModif.SetName(saveName.Text);
                saveModif.SetSource(saveSrcPath.Text);
                saveModif.SetDestination(saveDestPath.Text);
                this.m.afficher("edit");
            }
            
        }
        private void remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m.GetSaves().removeSave(saveModif);
                this.m.afficher("edit");
            }
            catch(Exception ex) 
            {
                System.Windows.MessageBox.Show(""+ex, "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void src_Click(object sender, RoutedEventArgs e)
        {
            saveSrcPath.Text = OpenFolderDialog();
        }

        public Object charger()
        {
            setSaveModif();
            return this.Content;
        }

        private void dst_Click(object sender, RoutedEventArgs e)
        {
            saveDestPath.Text = OpenFolderDialog();
        }
    }
}
