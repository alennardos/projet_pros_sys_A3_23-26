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
using System.Xml;
using WpfApp1;
using WpfApp1.src.vues;

namespace ConsoleApp1
{
    public partial class SettingsPageMenu : Page
    {
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
            /*
            combo_langages.SelectedIndex = 1;
            combo_typeLogs.SelectedIndex = 1;
            combo_crypt.SelectedIndex = 0;
            */
            loadLanguage();
            /*
            if(m.getMaxSize() != 0)
            {
                maxoctet.Text = m.getMaxSize().ToString();
            }
            */
            loadSettings();
            
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
           
            try
            {
                this.m.setExtensions(extensionString.Text);
                if (maxoctet.Text != "")
                {
                    m.setMaxSize(Int32.Parse(maxoctet.Text));
                }
                else
                {
                    m.setMaxSize(Int32.MaxValue);
                }
                saveSettings();
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

        public void saveSettings()
        {
            
            var settingsValues = new Dictionary<string, object>
            {
                { "language", combo_langages.SelectedValue },
                { "logsType", combo_typeLogs.SelectedValue },
                { "extensions", extensionString.Text },
                { "maxSize", maxoctet.Text },
                { "crypt", combo_crypt.SelectedValue }
            };
            

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EasySave\\settings\\settings.txt");

            /*
            if (!File.Exists(path))
            {
                
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteStartElement("saves");
                    writer.WriteEndElement();
                    writer.Close();
                }
            }
            */
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var kvp in settingsValues)
                {
                    writer.WriteLine($"{kvp.Key}:{kvp.Value}");
                }
            }
        }

        private void loadSettings()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EasySave\\settings\\settings.txt");

            string[] settingsValues;

            int nombreLignes = File.ReadLines(path).Count();

            settingsValues = new string[nombreLignes];

            using (StreamReader reader = new StreamReader(path))
            {
                string ligne;

                int index = 0;

                while ((ligne = reader.ReadLine()) != null)
                {
                    string[] elements = ligne.Split(':');
                    string valeur = elements[1].Trim();
                    settingsValues[index] = valeur;
                    index++;
                }
            }

            if (settingsValues[0] == "en")
            {
                combo_langages.SelectedIndex = 0;
            }
            else if (settingsValues[0] == "fr")
            {
                combo_langages.SelectedIndex = 1;
            }
            else
            {
                combo_langages.SelectedIndex = 1;
            }

            if (settingsValues[1] == "json")
            {
                combo_typeLogs.SelectedIndex = 1;
            }
            else if (settingsValues[1] == "xml")
            {
                combo_typeLogs.SelectedIndex = 0;
            }
            else
            {
                combo_typeLogs.SelectedIndex = 0;
            }

            if (settingsValues[4] == "true")
            {
                combo_crypt.SelectedIndex = 0;
            }
            else if (settingsValues[4] == "false")
            {
                combo_crypt.SelectedIndex = 1;
            }
            else
            {
                combo_crypt.SelectedIndex = 1;
            }

            maxoctet.Text = settingsValues[3];

            extensionString.Text = settingsValues[2];

        }

        private void DigitsOnlyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
