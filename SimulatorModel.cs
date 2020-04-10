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

        //from here I added till where i wrote!
        private double rudder;
        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
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


        //till here i added all above
        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, ushort port)
        {
            this.telnetClient.Connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            this.telnetClient.Disconnect();
        }

        public void start()
        {
            new Thread(delegate () {
                while (!stop)
                {

                    //get maps values

                    telnetClient.Write("get/position/latitude-deg");
                    Latitude = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/position/longitude-deg");
                    Longitude = Double.Parse(telnetClient.Read());


                    //get dashboard values

                    telnetClient.Write("get/instrumentation/airspeed-indicator/indicated-speed-kt");
                    Air_Speed = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/gps/indicated-altitude-ft");
                    Altitude = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/attitude-indicator/internal-roll-deg");
                    Roll = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/attitude-indicator/internal-pitch-deg");
                    Pitch = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/altimeter/indicated-altitude-ft");
                    Altimeter = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/heading-indicator/indicated-heading-deg");
                    Heading = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/gps/indicated-ground-speed-kt");
                    Ground_Speed = Double.Parse(telnetClient.Read());

                    telnetClient.Write("get/instrumentation/gps/indicated-vertical-speed");
                    Vertical_Speed = Double.Parse(telnetClient.Read());


                    //set

                    telnetClient.Write("set/controls/engines/current-engine/throttle");

                    telnetClient.Write("set/controls/flight/aileron");

                    telnetClient.Write("set/controls/flight/elevator");

                    telnetClient.Write("set/controls/flight/rudder");


                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();

        }

        //thats what I added:

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
