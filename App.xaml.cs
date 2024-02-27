using ConsoleApp1.src;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.src;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        void Application_Exit(object sender, ExitEventArgs e)
        {
            Saves s = Saves.Instance();
            s.quit();
        }

    }

}
