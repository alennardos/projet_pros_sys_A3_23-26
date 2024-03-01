﻿using System.Net.Sockets;
using System.Net;
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
using System.Text.RegularExpressions;

namespace clientProgress
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    //CLIENT MAINWINDOW
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Background = Brushes.LightGray;

        }

        private static void EcouterReseau(Socket s)
        {
            byte[] buf = new byte[4];
            s.Receive(buf);
            Client c = new Client(s);
            c.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip.Text), Int32.Parse(port.Text));
                s.Connect(ipep);
                EcouterReseau(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex);
            }
        }
        private void DigitsOnlyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}