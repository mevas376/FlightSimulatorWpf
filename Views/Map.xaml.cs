using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public Map()
        {
            InitializeComponent();
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(MyMap_Loaded);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        bool stopChanging = false;
        double mapCenterX = 0;
        double mapCenterY = 0;

        double VM_Latitude
        {
            get
            {
                return (DataContext as FlightSimulator.SimulatorViewModel).VM_Latitude;
            }
        }
        double VM_Longitude
        {
            get
            {
                return (DataContext as FlightSimulator.SimulatorViewModel).VM_Longitude;
            }
        }

        const double toleration = 1.8;
        const double offset = 1.5;
        public Location VM_MapCenter
        {
            get
            {
                if ((mapCenterX - VM_Longitude) > MyMap.BoundingRectangle.Width / toleration)
                {
                    mapCenterX = Math.Max(VM_Longitude - MyMap.BoundingRectangle.Width / offset, -180);
                }

                if ((VM_Longitude - mapCenterX) > MyMap.BoundingRectangle.Width / toleration)
                {
                    mapCenterX = Math.Min(VM_Longitude + MyMap.BoundingRectangle.Width / offset, 180);
                }

                if ((mapCenterY - VM_Latitude) > MyMap.BoundingRectangle.Height / toleration)
                {
                    mapCenterY = Math.Max(VM_Latitude - MyMap.BoundingRectangle.Height / offset, -90);
                }

                if ((VM_Latitude - mapCenterY) > MyMap.BoundingRectangle.Height / toleration)
                {
                    mapCenterY = Math.Min(VM_Latitude + MyMap.BoundingRectangle.Height / offset, 90);
                }

                return new Location(mapCenterY, mapCenterX);
            }
        }

        private void MyMap_Loaded(object sender, EventArgs e)
        {
            if (MyMap.IsLoaded && !stopChanging)
            {
               MyMap.SetView(VM_MapCenter, MyMap.ZoomLevel);
            }
        }

        private void MyMap_DragLeave(object sender, MouseButtonEventArgs e)
        {
            mapCenterY = MyMap.BoundingRectangle.Center.Altitude;
            mapCenterY = MyMap.BoundingRectangle.Center.Latitude;
            stopChanging = false;
        }

        private void MyMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stopChanging = true;
        }

        private void MyMap_Loaded_1(object sender, RoutedEventArgs e)
        {
            mapCenterY = VM_Latitude;
            mapCenterX = VM_Longitude;
        }
    }
}
