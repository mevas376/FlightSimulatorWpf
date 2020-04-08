using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public class SimulatorViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        public SimulatorViewModel(ISimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            (Object sender, PropertyChangedEventArgs e) =>
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) { }

        //properties:
        double VM_Rudder { get { return model.Rudder; } }

        public double VM_Elevator { get { return model.Elevator; } }
        public double VM_Aileron { get { return model.Aileron; } }
        public double VM_Throttle { get { return model.Throttle; } }
        public double VM_Indicated_Heading_Deg { get { return model.Indicated_Heading_Deg; } }
        public double VM_Gps_Indicated_Vertical_Speed { get { return model.Gps_Indicated_Vertical_Speed; } }
        public double VM_Gps_Indicated_Ground_Speed_Kt { get { return model.Gps_Indicated_Ground_Speed_Kt; } }
        public double VM_Airspeed_Indicator_Indicated_Speed_Kt { get { return model.Airspeed_Indicator_Indicated_Speed_Kt; } }
        public double VM_Gps_Indicated_Altitude_Ft { get { return model.Gps_Indicated_Altitude_Ft; } }
        public double VM_Attitude_Indicator_Internal_Roll_Deg { get { return model.Attitude_Indicator_Internal_Roll_Deg; } }

        public double VM_Atitude_Indicator_Internal_Pitch_Deg { get { return model.Atitude_Indicator_Internal_Pitch_Deg; } }

        public double VM_Altimeter_Indicated_Altitude_Ft { get { return model.Altimeter_Indicated_Altitude_Ft; } }

        public double VM_Position_Longitude_Deg { get { return model.Position_Latitude_Deg; } }
        public double VM_Position_Latitude_Deg { get { return model.Position_Latitude_Deg; } }


    }
}
