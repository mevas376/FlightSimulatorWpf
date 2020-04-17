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
       
        public MainWindow()
        {
            Console.WriteLine("startinggg MEVAS ROCKS!!!");
            InitializeComponent();
            SimulatorModel model = new SimulatorModel(new MyTelnetClient());
            vm = new SimulatorViewModel(model);
            DataContext = vm;
            model.connect("127.0.0.1", 5402);
            model.start();
        }
        private void Joystick_Loaded(object sender, RoutedEventArgs e) { }

      

    }
}

