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
                    telnetClient.Write("set /controls/flight/rudder " + ToString(value) + "\n");
                    telnetClient.Read(); // Discard

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

                    telnetClient.Write("set /controls/flight/elevator " + ToString(value) + "\n");
                    telnetClient.Read();    // Discard

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

                    telnetClient.Write("set /controls/flight/aileron " + ToString(value) + "\n");
                    telnetClient.Read();    // Discard

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
                    throttle = value;
                    telnetClient.Write("set /controls/engines/current-engine/throttle " + ToString(value) + "\n");
                    telnetClient.Read();    // Discard

                    NotifyPropertyChanged("Throttle");
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
                this.altitude = value;

                this.NotifyPropertyChanged("Altitude");
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                this.longitude = ((value + 180) % 360) - 180;

                this.NotifyPropertyChanged("Longitude");
                this.NotifyPropertyChanged("Location");
                this.NotifyPropertyChanged("MapCenter");
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                this.latitude = ((value + 90) % 180) - 90;

                this.NotifyPropertyChanged("Location");
                this.NotifyPropertyChanged("Latitude");
                this.NotifyPropertyChanged("MapCenter");
            }
        }

        private string error;
        public string Error
        {
            get { return this.error; }
            set
            {
                if (this.error != value)
                {
                    this.error = value;
                    Console.WriteLine("Error has been changed in MODEL");

                    this.NotifyPropertyChanged("Error");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, ushort port)
        {
            try
            {
                telnetClient.Connect(ip, port);
            }
            catch(IOExeption e)
            {
                Error = e.Message;
            }
           
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

                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();

        }


    }
}