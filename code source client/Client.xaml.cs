using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
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

namespace clientProgress
{
    /// <summary>
    /// Logique d'interaction pour Client.xaml
    /// </summary>
    public partial class Client : Window
    {

        Socket s;
        bool clickPlay;


        public void getValue(object sender, DoWorkEventArgs e)
        {
            byte[] value = new byte[128];
            bool run = true;
            int progress = 0;
            while (run)
            {
                if (clickPlay)
                {
                    s.Send(Encoding.UTF8.GetBytes("PlayPause"));
                    this.clickPlay = false;
                }
                else
                {
                    s.Send(Encoding.UTF8.GetBytes("normale"));
                }
                s.Receive(value);
                progress = BitConverter.ToInt32(value, 0);
                (sender as BackgroundWorker).ReportProgress(progress);
                run = progress != 100;
            }
            //TODO : demander Adam comment fermer la fenetre
        }

        public void setPlayPause()
        {
            this.clickPlay = true;
        }

        public Client(Socket s)
        {
            this.s = s;
            this.clickPlay = false;
            InitializeComponent();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += getValue;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //initialisation de la barre de progression avec le pourcentage de progression
            pbstatus1.Value = e.ProgressPercentage;

            //Affichage de la progression sur un label
            lb_etat_prog_server.Content = pbstatus1.Value.ToString() + "%";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.setPlayPause();
        }


    }
}
