using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPSupahChat
{
    public class UDPListener: UDPBase
    {
        private IPEndPoint listenOn;

        /*public UDPListener()
        {
        }*/

        public UDPListener( IPEndPoint endpoint ) {
            listenOn = endpoint;
            Client = new UdpClient(listenOn);
        }

        public void Reply(string message, IPEndPoint endpoint) {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length, endpoint);
        }
    }
}
