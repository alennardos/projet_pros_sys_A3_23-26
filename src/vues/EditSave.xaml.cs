﻿using ConsoleApp1.src;
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
        MainWindow m;

        public EditSave(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
        }
        

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.m.afficher("menu");
        }

        private void listeSaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void loadLanguage()
        {
            Edit.Content = m.GetResourceManager().GetString("EDIT_edit");
            Home.Content = m.GetResourceManager().GetString("home");
        }

        private void editSave_Click(object sender, RoutedEventArgs e)
        {
            if (listeSaves.SelectedIndex == -1)
            {
                
                System.Windows.MessageBox.Show(m.GetResourceManager().GetString("error_nosave"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.m.setSaveModif(listeSaves.SelectedIndex);
            this.m.afficher("secondEdit");

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

        public Object charger()
        {
            loadLanguage();
            addSavesListeSave();
            return this.Content;
        }

    }
}
