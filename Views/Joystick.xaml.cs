using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl, INotifyPropertyChanged
    {
        //data members:
        private Point centerPoint;
        private bool isPressed;

        private double RudderValue;
        private double ElevatorValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string RudderString
        {
            get
            {
                return RudderValue.ToString();
            }
        }
        private string ElevatorString 
        { 
            get
            {
                return ElevatorValue.ToString();
            }
        }
        public string AileronString
        {
            get
            {
                return "Aileron: " + string.Format("{0:0.00}", Aileron.Value);
            }
        }
        public string ThrottleString
        {
            get
            {
                return "Throttle: " + string.Format("{0:0.00}", Throttle.Value);
            }
        }


        public Joystick()
        {
            RudderValue = 0.0;
            ElevatorValue = 0.0;
            InitializeComponent();
            DataContext = this;

        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            Knob.CaptureMouse();
        }

        private void KnobBase_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void KnobBase_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NotifyPropertyChanged("ThrottleString");
        }
        private void Aileron_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NotifyPropertyChanged("AileronString");
        }
    }
}
