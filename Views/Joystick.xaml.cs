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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(RudderProperty)); }
            set {
                Console.WriteLine("rudder has been changed in JOYSTICK");
                SetValue(RudderProperty, value); 
            }
        }


        public static readonly DependencyProperty RudderProperty =
            DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);


        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProperty)); }
            set {
                Console.WriteLine("elevator has been changed in JOYSTICK");
                SetValue(ElevatorProperty, value); 
            }
        }

        public static readonly DependencyProperty ElevatorProperty =
    DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);



     /*   public double DiffFromLastPosition
        {
            get { return Convert.ToDouble(GetValue(DiffFromLastPositionProperty)); }
            set { SetValue(DiffFromLastPositionProperty, value); }
        }

        public static readonly DependencyProperty DiffFromLastPositionProperty =
            DependencyProperty.Register(" DiffFromLast", typeof(double), typeof(Joystick), null);*/




        private Point centerPoint;
        private bool isPressed;
        private double newX, newY;
        //members for the Animation
        private Storyboard sb;
        private DoubleAnimation x, y;



        public Joystick()
        {
            InitializeComponent();
            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseMove += Knob_MouseMove;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            centerPoint = new Point(Base.Width / 2, Base.Height / 2);
            isPressed = false;
            newX = newY = 0;
            sb = Knob.Resources["CenterKnob"] as Storyboard;
            x = sb.Children[0] as DoubleAnimation;
            y = sb.Children[1] as DoubleAnimation;
            x.From = 0;
            y.From = 0;
        }
        private void centerKnob_Completed(object sender, EventArgs e)
        {
    
        }


        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            Knob.CaptureMouse();
        }

        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
            Knob.ReleaseMouseCapture();
            //the Animation of returning the kobBase to the center
            newX = 0;
            newY = 0;
            x.To = newX;
            y.To = newY;
            sb.Begin();
            x.From = x.To;
            y.From = y.To;
            Rudder = Elevator = 0;
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                newX = e.GetPosition(Base).X;
                newY = e.GetPosition(Base).Y;
                //cheak if the knobBase is in Bound
                double bound = Math.Sqrt(Math.Pow(newX - centerPoint.X, 2) + Math.Pow(newY - centerPoint.Y, 2));
                if (bound > (this.Base.Width / 2) - (KnobBase.Width / 2) || bound > (this.Base.Height / 2) - (KnobBase.Height / 2))
                {
                    return;
                }
                else
                {
                    Rudder = (newX - centerPoint.X) / (Base.Width / 2 - KnobBase.Width / 2);
                    Elevator = -((newY - centerPoint.Y) / (Base.Width / 2 - KnobBase.Width / 2));
                    //the Animation
                    y.To = newY - centerPoint.Y;
                    x.To = newX - centerPoint.X;
                    sb.Begin();
                    x.From = x.To;
                    y.From = y.To;

                    //double diff = Math.Sqrt(Math.Pow(x.To.GetValueOrDefault() - x.From.GetValueOrDefault(), 2) + Math.Pow(y.To.GetValueOrDefault() - y.From.GetValueOrDefault(), 2));
                    //Console.WriteLine("diff " + diff);
                   /* if (diff > 1)
                    {
                        Console.WriteLine("In diff:  " + diff);
                        DiffFromLastPosition = diff;
                    }*/

                }
            }
        }
    }
}
