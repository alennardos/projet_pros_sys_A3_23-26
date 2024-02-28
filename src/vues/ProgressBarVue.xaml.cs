using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using ProgressBar = System.Windows.Controls.ProgressBar;

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour ProgressBar.xaml
    /// </summary>
    public partial class ProgressBarVue : Window
    {

        private Save s;
        ResourceManager rm;
        bool isPlaying = true;

        private static void AccepterConnexion(Object list)
        {
            Socket res = ((Socket)((List<Object>)list)[0]).Accept();
            sendProgress(res, (System.Windows.Controls.ProgressBar)((List<Object>)list)[1]);
        }

        private static void sendProgress(Socket s, ProgressBar pb)
        {
            byte[] value = new byte[128];
            bool run = true;
            int i = 0;
            while (run)
            {
                // Utilise Dispatcher.Invoke pour accéder à l'interface utilisateur depuis le thread UI
                pb.Dispatcher.Invoke(() =>
                {
                    byte[] value = BitConverter.GetBytes(i);
                    s.Send(value);
                    Thread.Sleep(1000);
                    i++;
                    //run = pb.Value == 100; // Si vous avez besoin d'arrêter le bouclage lorsque la valeur atteint 100, vous pouvez le faire ici
                });
            }
        }
        

        public ProgressBarVue(Save s, Socket socket, ResourceManager rm)
        {
            this.rm = rm;
            InitializeComponent();
            this.s = s;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += s.progress;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
            Thread serveur = new Thread(AccepterConnexion);

            serveur.Start(new List<object>() { socket, pbstatus1 });

            this.rm = rm;
            etatProgressLabel.Content = rm.GetString("LAUNCH_progress");
            playPause.Content = rm.GetString("LAUNCH_play");
        }

            void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            //Initializing the progress bar with the progress percentage.
            pbstatus1.Value = e.ProgressPercentage;

            //close when 100%
            if(pbstatus1.Value == 100)
            {
                System.Windows.MessageBox.Show("Le processus est terminé.", "Fin de traitement", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }

            //Displaying the progress on a label.
            etatProgressLabel.Content = pbstatus1.Value.ToString() + "%";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.s.pausePlay();
            if (isPlaying)
            {
                isPlaying = false;
                playPause.Content = rm.GetString("LAUNCH_pause");
            }
            else
            {
                isPlaying = true;
                playPause.Content = rm.GetString("LAUNCH_play");
            }
        }

    }
}
