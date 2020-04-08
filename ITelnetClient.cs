using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public interface ITelnetClient
    {
        void Connect(string ip, UInt16 port);
        void Write(string command);
        string Read();
        void Disconnect();
    }
}
