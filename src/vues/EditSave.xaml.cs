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
            edit_edit editWindow = new edit_edit();

            // Créer une fenêtre Popup
            Popup popup = new Popup();
            popup.Child = editWindow;

            // Définir les propriétés de la fenêtre popup
            popup.Width = 400; // Ajustez la largeur selon vos besoins
            popup.Height = 300; // Ajustez la hauteur selon vos besoins
            popup.VerticalOffset = 100; // Ajustez la position verticale selon vos besoins
            popup.HorizontalOffset = 100; // Ajustez la position horizontale selon vos besoins
            popup.IsOpen = true;

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
