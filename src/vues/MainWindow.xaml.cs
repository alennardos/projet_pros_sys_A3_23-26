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
        CreateSave vueSave;
        Home vueHome;
        public MainWindow()
        {
            InitializeComponent();
            vueSave = new CreateSave(this);
            vueHome = new Home(this);
            this.Content = vueHome;
        }

        public void afficher(string page)
        {
            switch(page)
            {
                case "menu":
                    this.Content = vueHome;
                    break;
                case "create":
                    this.Content = vueSave;
                    break;
            }
        }
    }
}