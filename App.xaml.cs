using ConsoleApp1.src;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
            System.Diagnostics.Process.GetCurrentProcess().Kill();

            //kill all process of the app when the user exit the app
            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process pro in processes)
            {
                if (pro.Id != Process.GetCurrentProcess().Id)
                {
                    pro.Kill();
                }
            }

        }

    }

}
