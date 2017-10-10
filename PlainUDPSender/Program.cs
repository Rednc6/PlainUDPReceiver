using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainUDPSender
{
    class Program
    {
        private const int PORT = 7007;
        static void Main(string[] args)
        {
            UDPSender udp = new UDPSender(PORT);
            udp.Start();

            Console.ReadKey();
        }
    }
}
