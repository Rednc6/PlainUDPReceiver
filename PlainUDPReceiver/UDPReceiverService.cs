using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PlainUDPReceiver
{
    internal class UDPReceiverService
    {
        private readonly int PORT;

        public UDPReceiverService(int port)
        {
            PORT = port;
        }

        public void Start()
        {
            byte[] buffer;

            using (UdpClient udp = new UdpClient(PORT))
            {
                IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0); // lige meget, bliver sat senere

                buffer = udp.Receive(ref senderEP); // modtager adresse
                Console.WriteLine($"Datagram lenght : {buffer.Length}");
                Console.WriteLine($"Sender IP : {senderEP.Address}, Port : {senderEP.Port}");

                string incStr = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(incStr);

                // send back
                string outStr = incStr.ToUpper();
                byte[] outbuffer = Encoding.ASCII.GetBytes(outStr);

                udp.Send(outbuffer, outbuffer.Length, senderEP);
            }
        }
    }
}
