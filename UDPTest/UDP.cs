using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPTest
{
    class UDP
    {
        private UdpClient udpClient = null;
        private IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        private ushort portNumber = 50000;
        public void MLoop()
        {
            double Pitch, Roll;
            udpClient = new UdpClient(new IPEndPoint(localAddress, portNumber));
            
            IPEndPoint senderEndPoint = new IPEndPoint(localAddress, 0);
            while (true)
            {

                while (udpClient.Available > 24)
                {
                    byte[] DiscardBuffer = udpClient.Receive(ref senderEndPoint);
                }
               byte[] receiveBuffer;

                receiveBuffer = udpClient.Receive(ref senderEndPoint);
                double PtchData = double.Parse(receiveBuffer[3].ToString()) + double.Parse(receiveBuffer[2].ToString()) / 256;
               
                if (PtchData >= 128)                //Climb
                    Pitch = Math.Pow(5.2419, (PtchData - 191.954));
                else                                // Dive
                    Pitch = -Math.Pow(4.559, (PtchData - 63.7433)); 
                Console.Write("Pitch    ");
                Console.Write(Pitch);
                Console.WriteLine("      ");
               // Thread.Sleep(10);
               
                double RollData = double.Parse(receiveBuffer[15].ToString()) + double.Parse(receiveBuffer[14].ToString()) / 256;
                if (RollData >= 128)            // Roll Right
                    Roll = Math.Pow(6.479, (RollData - 192.418));
                else                             // Roll Left
                    Roll = -Math.Pow(5.21,(RollData - 64.0534));
                Console.Write("                                        Roll     ");
                Console.Write(Roll);
                Console.WriteLine("      ");
               // Thread.Sleep(10);
            }
        }
    }   
  }


