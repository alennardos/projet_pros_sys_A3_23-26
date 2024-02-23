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
    /// Logique d'interaction pour EditSave.xaml
    /// </summary>
    public partial class EditSave : Page
    {
        public EditSave(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
        }
        MainWindow m;

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.m.afficher("menu");
        }

        private void listeSaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editSave_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre edit_edit
            edit_edit popup = new edit_edit();
            
            popup.Width = 400; 
            popup.Height = 300;
            popup.Show();

        }

        public void addSavesListeSave()
        {
            listeSaves.Items.Clear();
            ResourceManager rm = this.m.GetResourceManager();
            listeSaves.SelectionMode = SelectionMode.Single;
            foreach (Save s in m.GetSaves().getSaves())
            {
                listeSaves.Items.Add($"{rm.GetString("SAVE_name")}:     {s.GetName()},     {rm.GetString("SAVE_src")}: {s.GetSource()},     {rm.GetString("SAVE_dest")}: {s.GetDestination()}");
            }
        }


    }
}
