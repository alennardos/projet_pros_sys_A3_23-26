using ConsoleApp1.src;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProgressBar(Save s)
        {
            InitializeComponent();
            this.s = s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.s.pausePlay();
        }
    }
}
