using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ModelLib;

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

        public void StartXML()
        {
            byte[] buffer;

            using (UdpClient udp = new UdpClient(PORT))
            {
                IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0); // lige meget, bliver sat senere

                buffer = udp.Receive(ref senderEP); // modtager adresse

                Console.WriteLine($"Datagram lenght : {buffer.Length}");
                Console.WriteLine($"Sender IP : {senderEP.Address}, Port : {senderEP.Port}");

                string incStr = Encoding.ASCII.GetString(buffer);

                XmlSerializer xs = new XmlSerializer(typeof(Car));
                StringReader sr = new StringReader(incStr);
                var deserializedObj = xs.Deserialize(sr);
                Car car = (Car)deserializedObj;

                Console.WriteLine($"Model : {car.Model}, Color : {car.Color}, RegNo : {car.RegNo}");

                // send back
                string outStr = incStr.ToUpper();
                byte[] outbuffer = Encoding.ASCII.GetBytes(outStr);

                udp.Send(outbuffer, outbuffer.Length, senderEP);
            }
        }
    }
}
