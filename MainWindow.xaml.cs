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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SimulatorViewModel vm;
        MyTelnetClient client;
        public MainWindow()
        {
            InitializeComponent();
            //   vm = new SimulatorViewModel(new SimulatorModel(new MyTelnetClient()));
            // DataContext = vm;
            client = new MyTelnetClient();
            client.Connect("127.0.0.1", 8080);
            Closed +=
                (o, p) =>
                {
                    client.Disconnect();
                };
        }

        

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            string s = input.Text;
            client.Write(s);
            input.Text = "";

        }
        private void ReceiveButtonClick(object sender, RoutedEventArgs e)
        {
            output.Content = client.Read();
        }
    }
}

