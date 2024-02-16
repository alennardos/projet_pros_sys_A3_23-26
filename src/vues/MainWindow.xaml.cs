using ConsoleApp1.src;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Console_Application_Test_1.src;
using WpfApp1.src.vues;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewModelSave vm = ViewModelSave.Instance;

        public MainWindow()
        {
            InitializeComponent();
            create.Content = ViewModelSave.Instance.GetResourceManager().GetString("HOME_create_save");
            lunch.Content = ViewModelSave.Instance.GetResourceManager().GetString("HOME_lunch_save");
            edit.Content = ViewModelSave.Instance.GetResourceManager().GetString("HOME_edit_save");
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            CreateSave cs = new CreateSave(this);
            this.Content = cs;
        }

        private void lunch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}