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
        static int value = 0;

        private static void AccepterConnexion(Object list)
        {
            Socket res = ((Socket)((List<Object>)list)[0]).Accept();
            res.Send(Encoding.UTF8.GetBytes("Connected"));
            sendProgress(res, ((Save)((List<Object>)list)[1]));
        }

        private static void sendProgress(Socket s, Save save)
        {
            byte[] buffer = new byte[1024];
            String message;
            bool run = true;
            while (run)
            {
                //try
                //{
                    s.Receive(buffer);
                    message = Encoding.UTF8.GetString(buffer);

                    if (message.Contains("PlayPause"))
                    {
                        save.pausePlay();
                    }

                    s.Send(BitConverter.GetBytes(value));
                    run = value != 100;
                    Thread.Sleep(300);
                //}
                //catch  (Exception ex)
                //{
                //    run = false;
                //}
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

            serveur.Start(new List<object>() { socket, s});

            this.rm = rm;
            progressLabel.Content = rm.GetString("LAUNCH_progress");
            playPause.Content = rm.GetString("LAUNCH_play");
            saveNameLabel.Content = s.GetName();
            
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            //Initializing the progress bar with the progress percentage.
            pbstatus1.Value = e.ProgressPercentage;

            value = (int)pbstatus1.Value;

            etatProgressLabel.Content = pbstatus1.Value.ToString() + "%";

            //close when 100%
            if (pbstatus1.Value == 100)
            {
                System.Windows.MessageBox.Show(s.GetName() + " : " + rm.GetString("LAUNCH_succes"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.detected == true)
            {
                playPause.Content = rm.GetString("LAUNCH_pause");
                System.Windows.MessageBox.Show(rm.GetString("error_business_software"), "EasySave", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
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
}
