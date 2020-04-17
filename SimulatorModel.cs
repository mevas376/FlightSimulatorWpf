using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace FlightSimulator
{
    public class SimulatorModel : ISimulatorModel
    {

        ITelnetClient telnetClient;

        volatile Boolean stop;

        public SimulatorModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
        }

        private double rudder;
        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
                    Console.WriteLine("rudder has been changed in MODEL");

                    this.NotifyPropertyChanged("Rudder");
                }
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return this.elevator; }
            set
            {
                if (this.elevator != value)
                {
                    this.elevator = value;
                    Console.WriteLine("elevator has been changed in MODEL");

                    this.NotifyPropertyChanged("Elevator");
                }
            }
        }

        private double aileron;
        public double Aileron
        {
            get { return this.aileron; }
            set
            {
                if (this.aileron != value)
                {
                    this.aileron = value;
                    Console.WriteLine("aileron has been changed in MODEL");

                    this.NotifyPropertyChanged("Aileron");
                }
            }
        }

        private double throttle;
        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                if (this.throttle != value)
                {
                    this.throttle = value;
                    Console.WriteLine("throttle has been changed in MODEL");

                    this.NotifyPropertyChanged("Throttle");
                }
            }
        }

        private double heading;
        public double Heading
        {
            get { return this.heading; }
            set
            {
                if (this.heading != value)
                {
                    this.heading = value;
                    Console.WriteLine("heading has been changed in MODEL");

                    this.NotifyPropertyChanged("Heading");
                }
            }
        }
        private double vertical_speed;
        public double Vertical_Speed
        {
            get { return this.vertical_speed; }
            set
            {
                if (this.vertical_speed != value)
                {
                    this.vertical_speed = value;
                    Console.WriteLine("vertical_speed has been changed in MODEL");

                    this.NotifyPropertyChanged("Vertical_Speed");
                }
            }
        }
        private double ground_speed;
        public double Ground_Speed
        {
            get { return this.ground_speed; }
            set
            {
                if (this.ground_speed != value)
                {
                    this.ground_speed = value;
                    Console.WriteLine("ground_speed has been changed in MODEL");
                    this.NotifyPropertyChanged("Ground_Speed");
                }
            }
        }

        private double air_speed;
        public double Air_Speed
        {
            get { return this.air_speed; }
            set
            {
                if (this.air_speed != value)
                {
                    this.air_speed = value;
                    Console.WriteLine("Air_Speed has been changed in MODEL");
                    this.NotifyPropertyChanged("Air_Speed");
                }
            }
        }

        private double altimeter;
        public double Altimeter
        {
            get { return this.altimeter; }
            set
            {
                if (this.altimeter != value)
                {
                    this.altimeter = value;
                    Console.WriteLine("Altimeter has been changed in MODEL");

                    this.NotifyPropertyChanged("Altimeter");
                }
            }
        }

        private double roll;
        public double Roll
        {
            get { return this.roll; }
            set
            {
                if (this.roll != value)
                {
                    this.roll = value;
                    Console.WriteLine("Roll has been changed in MODEL");
                    this.NotifyPropertyChanged("Roll");
                }
            }
        }


        private double pitch;
        public double Pitch
        {
            get { return this.pitch; }
            set
            {
                if (this.pitch != value)
                {
                    this.pitch = value;
                    Console.WriteLine("Pitch has been changed in MODEL");

                    this.NotifyPropertyChanged("Pitch");
                }
            }
        }

        private double altitude;
        public double Altitude
        {
            get { return this.altitude; }
            set
            {
                if (this.altitude != value)
                {
                    this.altitude = value;
                    Console.WriteLine("Altitude has been changed in MODEL");

                    this.NotifyPropertyChanged("Altitude");
                }
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if (this.longitude != value)
                {
                    this.longitude = value;
                    Console.WriteLine("longitude has been changed in MODEL");
                    this.NotifyPropertyChanged("Longitude");
                }
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                if (this.latitude != value)
                {
                    this.latitude = value;
                    Console.WriteLine("Latitude has been changed in MODEL");

                    this.NotifyPropertyChanged("Latitude");
                }
            }
        }

        public double Indicated_Heading_Deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Gps_Indicated_Vertical_Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Gps_Indicated_Ground_Speed_Kt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Airspeed_Indicator_Indicated_Speed_Kt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Gps_Indicated_Altitude_Ft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Attitude_Indicator_Internal_Roll_Deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Atitude_Indicator_Internal_Pitch_Deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Altimeter_Indicated_Altitude_Ft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Position_Longitude_Deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Position_Latitude_Deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, ushort port)
        {
            telnetClient.Connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            telnetClient.Disconnect();
        }

        private string ToString(double throttle)
        {
            return Throttle.ToString();
        }

        //thats what I added:

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void start()
        {
            new Thread(delegate () {
                while (!stop)
                {
                    //get maps values

                    telnetClient.Write("get /position/latitude-deg\n");
                    Latitude = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /position/longitude-deg\n");
                    Longitude = Double.Parse(telnetClient.Read());


                    //get dashboard values

                    telnetClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    Air_Speed = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/gps/indicated-altitude-ft\n");
                    Altitude = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Roll = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Pitch = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    Heading = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    Ground_Speed = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get /instrumentation/gps/indicated-vertical-speed\n");
                    Vertical_Speed = Double.Parse(telnetClient.Read());


                    //set
                    //to put in a different method, to be callled only if something has changed.



                    //NEED TO CHECK WHY HE ASKED ME TO IMPLEMENT A TOSTRING METHOD JUST FOR THROTTLE!!!
                    //something here is not right...
                    telnetClient.Write("set /controls/engines/current-engine/throttle " + ToString(Throttle) + "\n");
                    Throttle = Double.Parse(telnetClient.Read());

                    telnetClient.Write("set /controls/flight/aileron " + ToString(Aileron) + "\n");
                    Aileron = Double.Parse(telnetClient.Read());

                    telnetClient.Write("set /controls/flight/elevator " + ToString(Elevator) + "\n");
                    Elevator = Double.Parse(telnetClient.Read());

                    telnetClient.Write("set /controls/flight/rudder " + ToString(Rudder) + "\n");
                    Rudder = Double.Parse(telnetClient.Read());

                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();

        }
    }
}
