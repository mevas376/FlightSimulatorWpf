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
        double Heading { set; get; }
        double Vertical_Speed { set; get; }
        double Ground_Speed { set; get; }
        double Air_Speed { set; get; }
        double Altimeter { set; get; }
        double Roll { set; get; }

        double Pitch { set; get; }

        double Altitude { set; get; }

        double Longitude { set; get; }
        double Latitude { set; get; }
    }
}
