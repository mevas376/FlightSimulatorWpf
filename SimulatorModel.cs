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
        public double Elevator {
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
        public double Aileron {
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

        private double indicated_heading_deg;
        public double Indicated_Heading_Deg
        {
            get { return this.indicated_heading_deg; }
            set
            {
                if (this.indicated_heading_deg != value)
                {
                    this.indicated_heading_deg = value;
                    this.NotifyPropertyChanged("Indicated_Heading_Deg");
                }
            }
        }
        private double gps_indicated_vertical_speed;
        public double Gps_Indicated_Vertical_Speed {
            get { return this.gps_indicated_vertical_speed; }
            set
            {
                if (this.gps_indicated_vertical_speed != value)
                {
                    this.gps_indicated_vertical_speed = value;
                    this.NotifyPropertyChanged("Gps_Indicated_Vertical_Speed");
                }
            }
        }
        private double gps_indicated_ground_speed_kt;
        public double Gps_Indicated_Ground_Speed_Kt {
            get { return this.gps_indicated_ground_speed_kt; }
            set
            {
                if (this.gps_indicated_ground_speed_kt != value)
                {
                    this.gps_indicated_ground_speed_kt = value;
                    this.NotifyPropertyChanged("ps_Indicated_Ground_Speed_Kt");
                }
            }
        }
        private double airspeed_indicator_indicated_speed_kt;
        public double Airspeed_Indicator_Indicated_Speed_Kt {
            get { return this.airspeed_indicator_indicated_speed_kt; }
            set
            {
                if (this.airspeed_indicator_indicated_speed_kt != value)
                {
                    this.airspeed_indicator_indicated_speed_kt = value;
                    this.NotifyPropertyChanged("Airspeed_Indicator_Indicated_Speed_Kt");
                }
            }
        }

        private double gps_indicated_altitude_ft;
        public double Gps_Indicated_Altitude_Ft {
            get { return this.gps_indicated_altitude_ft; }
            set
            {
                if (this.gps_indicated_altitude_ft != value)
                {
                    this.gps_indicated_altitude_ft = value;
                    this.NotifyPropertyChanged("Gps_Indicated_Altitude_Ft");
                }
            }
        }

        private double attitude_indicator_internal_roll_deg;
        public double Attitude_Indicator_Internal_Roll_Deg {
            get { return this.attitude_indicator_internal_roll_deg; }
            set
            {
                if (this.attitude_indicator_internal_roll_deg != value)
                {
                    this.attitude_indicator_internal_roll_deg = value;
                    this.NotifyPropertyChanged("Attitude_Indicator_Internal_Roll_Deg");
                }
            }
        }


        private double atitude_indicator_internal_pitch_deg;
        public double Atitude_Indicator_Internal_Pitch_Deg {
            get { return this.atitude_indicator_internal_pitch_deg; }
            set
            {
                if (this.atitude_indicator_internal_pitch_deg != value)
                {
                    this.atitude_indicator_internal_pitch_deg = value;
                    this.NotifyPropertyChanged("Atitude_Indicator_Internal_Pitch_Deg");
                }
            }
        }

        private double altimeter_indicated_altitude_ft;
        public double Altimeter_Indicated_Altitude_Ft {
            get { return this.altimeter_indicated_altitude_ft; }
            set
            {
                if (this.altimeter_indicated_altitude_ft != value)
                {
                    this.altimeter_indicated_altitude_ft = value;
                    this.NotifyPropertyChanged("Altimeter_Indicated_Altitude_Ft");
                }
            }
        }

        private double position_longitude_deg;
        public double Position_Longitude_Deg
        {
            get { return this.position_longitude_deg; }
            set
            {
                if (this.position_longitude_deg != value)
                {
                    this.position_longitude_deg = value;
                    this.NotifyPropertyChanged("Position_Longitude_Deg");
                }
            }
        }

        private double position_latitude_deg;
        public double Position_Latitude_Deg {
            get { return this.position_latitude_deg; }
            set
            {
                if (this.position_latitude_deg != value)
                {
                    this.position_latitude_deg = value;
                    this.NotifyPropertyChanged("Position_latitude_deg");
                }
            }
        }


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
                    telnetClient.Write("get left sonar");
                    Rudder = Double.Parse(telnetClient.Read());
                    // the same for the other sensors properties

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
