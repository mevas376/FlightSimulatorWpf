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

        public void NotifyPropertyChanged(string propName) 
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //properties:
      /*  private double diff;
        double VM_Diff
        {
            set
            {
                if (this.diff != value)

                    this.diff = value;
            }
        }*/

        private double rudder;
        public double VM_Rudder
        {
            set
            {
                if (this.rudder != value)

                    this.rudder = value;
                this.model.Rudder = value;
            }
            get { return model.Rudder; } }



        private double elevator;
        public double VM_Elevator
        {
            set
            {
                if (this.elevator != value)

                    this.elevator = value;
                this.model.Elevator = value;
            }
            get { return model.Elevator; } }

        private double aileron;
        public double VM_Aileron
        {
            set
            {
                if (this.aileron != value)

                    this.aileron = value;
                this.model.Aileron = value;
            }
            get { return model.Aileron; } }

        private double throttle;
        public double VM_Throttle
        {
            set
            {
                if (this.throttle != value)

                    this.throttle = value;
                this.model.Throttle = value;
            }
            get { return model.Throttle; }
        }

        public double VM_Heading { get { return model.Heading; } }
        public double VM_Vertical_Speed { get { return model.Vertical_Speed; } }
        public double VM_Ground_Speed { get { return model.Ground_Speed; } }
        public double VM_Air_Speed { get { return model.Air_Speed; } }
        public double VM_Altimeter { get { return model.Altimeter; } }
        public double VM_Roll { get { return model.Roll; } }

        public double VM_Pitch { get { return model.Pitch; } }

        public double VM_Altitude { get { return model.Altitude; } }

        public double VM_Longitude { get { return model.Longitude; } }
        public double VM_Latitude { get { return model.Latitude; } }

    }
}
