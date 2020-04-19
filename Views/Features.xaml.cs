using FlightSimulator;
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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Features.xaml
    /// </summary>
    public partial class Features : UserControl
    {
        public Features()
        {
            InitializeComponent();
        }

        //connect button
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SimulatorViewModel).Connect();
        }

        //disconnect button
        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SimulatorViewModel).Disconnect();
        }

        // clear logs button
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ErrorsBlock.Text = String.Empty;
        }
    }
}