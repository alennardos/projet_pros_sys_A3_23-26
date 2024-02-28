using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace WpfApp1.src.vues
{
    /// <summary>
    /// Logique d'interaction pour ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {

        private Save s;
        ResourceManager rm;
        bool isPlaying = true;

        public ProgressBar(Save s, ResourceManager rm)
        {
            this.rm = rm;
            InitializeComponent();
            this.s = s;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += s.progress;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
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
