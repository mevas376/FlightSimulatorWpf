using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public interface ISimulatorModel : INotifyPropertyChanged
    {
        void connect(string ip, ushort host);
        void disconnect();
        void start();

        double Rudder { set; get; }
        double Elevator { set; get; }
        double Aileron { set; get; }
        double Throttle { set; get; }
        double Indicated_Heading_Deg { set; get; }
        double Gps_Indicated_Vertical_Speed { set; get; }
        double Gps_Indicated_Ground_Speed_Kt { set; get; }
        double Airspeed_Indicator_Indicated_Speed_Kt { set; get; }
        double Gps_Indicated_Altitude_Ft { set; get; }
        double Attitude_Indicator_Internal_Roll_Deg { set; get; }

        double Atitude_Indicator_Internal_Pitch_Deg { set; get; }

        double Altimeter_Indicated_Altitude_Ft { set; get; }

        double Position_Longitude_Deg { set; get; }
        double Position_Latitude_Deg { set; get; }
    }
}
