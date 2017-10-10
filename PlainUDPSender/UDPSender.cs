using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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
            Car car = new Car("Black", "Tesla", 123456);
            byte[] buffer = Encoding.ASCII.GetBytes(car.ToString()); // just sends the model obj as a string, aka useless.

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

        public void StartXML()
        {
            Car car = new Car("Black", "Tesla", 123456);
            
           XmlSerializer xs = new XmlSerializer(typeof(Car));
           StringWriter sw = new StringWriter();
           xs.Serialize(sw, car);
           var serializedObj = sw.ToString();

            
            byte[] buffer = Encoding.ASCII.GetBytes(serializedObj); // works? who knows.

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
