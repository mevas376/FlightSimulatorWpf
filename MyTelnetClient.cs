using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator
{
    public class MyTelnetClient : ITelnetClient
    {
        Socket socket = null;
        string buffer = "";
        public void Connect(string ip, ushort port)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("Establishing Connection to {0}", ip);
                socket.Connect(ip, port);
                Console.WriteLine("Connection established");
            }

            catch (Exception e)
            {

                Console.WriteLine("Unexpected exception : {0}", e.ToString());

            }

        }

        private string GetLine()
        {
            var index = buffer.IndexOf("\r\n");
            if (index != -1)
            {
                var line = buffer.Substring(0, index);
                buffer = buffer.Substring(index + 2);
                return line;
            }
            if (!socket.Connected)
            {
                var line = buffer;
                buffer = "";
                return line;
            }
            return null;
        }

        private string ReadSingleLine()
        {
            var result = GetLine();

            while (result == null)
            {
                byte[] currentBuffer = new byte[1024];
                socket.Receive(currentBuffer);
                buffer += Encoding.ASCII.GetString(currentBuffer);
                result = GetLine();
            }

            return result;
        }

        public void Disconnect()
        {
            if (socket == null)
            {
                throw new ArgumentException("Socket was not opened"); // TODO change
            }
            socket.Close();
        }

        public string Read()
        {
            if (socket == null)
            {
                throw new ArgumentException("Socket was not opened"); // TODO change
            }
            return ReadSingleLine();
        }

        public void Write(string command)
        {
            if (socket == null)
            {
                throw new ArgumentException("Socket was not opened"); // TODO change
            }
            socket.Send(Encoding.ASCII.GetBytes(command));
            socket.Send(Encoding.ASCII.GetBytes("\r\n"));
        }
    }
}
