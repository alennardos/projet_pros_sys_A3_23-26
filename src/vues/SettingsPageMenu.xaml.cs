using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfApp1;
using WpfApp1.src.vues;

namespace ConsoleApp1
{
    public partial class SettingsPageMenu : Page
    {
        Saves saves;
        MainWindow m;

        public SettingsPageMenu(MainWindow m)
        {
            InitializeComponent();

            this.m = m;

            combo_typeLogs.Items.Add("xml");
            combo_typeLogs.Items.Add("json");

            combo_crypt.Items.Add("true");
            combo_crypt.Items.Add("false");

            this.addLanguages();

            combo_langages.SelectedIndex = 1;
            combo_typeLogs.SelectedIndex = 1;
            combo_crypt.SelectedIndex = 0;

            loadLanguage();

            if(m.getMaxSize() != 0)
            {
                maxoctet.Text = m.getMaxSize().ToString();
            }
            
        }

        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        private void combo_langages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m.changeLanguage(combo_langages.SelectedItem as string);

            loadLanguage();
        }

        private void combo_typeLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m.changeLogTypes(combo_typeLogs.SelectedItem as string);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                this.m.setExtensions(extensionString.Text);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(m.GetResourceManager().GetString("error_extension"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

            if (maxoctet.Text != "")
            {

                try
                {
                    this.m.setExtensions(extensionString.Text);
                    m.setMaxSize(Int32.Parse(maxoctet.Text));
                    this.m.afficher("menu");
                }
                catch (FormatException)
                {
                    System.Windows.MessageBox.Show(m.GetResourceManager().GetString("erreur_format"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (OverflowException)
                {
                    System.Windows.MessageBox.Show("erreur_overflow", "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                this.m.afficher("menu");
            }
            */

            try
            {
                this.m.setExtensions(extensionString.Text);
                if (maxoctet.Text != "")
                {
                    this.m.setExtensions(extensionString.Text);
                    m.setMaxSize(Int32.Parse(maxoctet.Text));
                }
                this.m.afficher("menu");
            }
            catch (FormatException)
            {
                System.Windows.MessageBox.Show(m.GetResourceManager().GetString("erreur_format"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                System.Windows.MessageBox.Show("erreur_overflow", "EasySave", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(m.GetResourceManager().GetString("error_extension"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

            private void addLanguages()
        {
            var path = GetThisFilePath();

            DirectoryInfo dirLang = new DirectoryInfo(path + "\\..\\..\\..\\languages");

            bool skip = false;

            foreach (FileInfo file in dirLang.GetFiles())
            {
                if (skip)
                {
                    combo_langages.Items.Add(file.Name.Split('.')[0]);
                }
                skip = !skip;
            }
        }

        private void loadLanguage()
        {
            label_langage.Content = m.GetResourceManager().GetString("SETTINGS_lang");
            label_logType.Content = m.GetResourceManager().GetString("SETTINGS_log_type");
            label_crypt.Content = m.GetResourceManager().GetString("SETTINGS_crypt");
            home.Content = m.GetResourceManager().GetString("home");
            label_maxoctet.Content = m.GetResourceManager().GetString("SETTINGS_ko");
            extensionsLabel.Content = m.GetResourceManager().GetString("SETTINGS_extensions");
        }

        public Object charger()
        {
            return this.Content;
        }

        private void combo_crypt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String value = combo_crypt.SelectedItem as string;

            m.changeCrypt(value.Equals("true"));
        }

        private void maxoctet_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void DigitsOnlyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
