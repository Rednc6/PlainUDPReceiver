using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib;

namespace PlainUDPSender
{
    internal class UDPSender
    {
        private readonly int PORT;

        public UDPSender(int port)
        {
            PORT = port;
        }

        public void Start()
        {
            // Car car = new Car("Black", "Tesla", 123456); // use later
            String senderStr = "Michael";
            byte[] buffer = Encoding.ASCII.GetBytes(senderStr);

            using (UdpClient udp = new UdpClient())
            {
                IPEndPoint otherEp = new IPEndPoint(IPAddress.Broadcast, PORT);
                udp.Send(buffer, buffer.Length, otherEp);

                IPEndPoint receiverEp = new IPEndPoint(IPAddress.Any, PORT);
                byte[] receiverBuffer = udp.Receive(ref receiverEp);

                Console.WriteLine($"Datagram length : {receiverBuffer.Length}");
                String incStr = Encoding.ASCII.GetString(receiverBuffer);
                Console.WriteLine(incStr);
            }
        }
    }
}
