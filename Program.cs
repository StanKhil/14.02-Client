using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace OurClient
{
    internal class UDPCLient
    {
        static void Main()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Loopback, 5000));
            Console.WriteLine("Currency: EURO, DOLLAR, LEI, POUND, GRIVNA");
            while (true)
            {
                try
                {
                    Console.Write("Enter a currency 1: ");
                    string message = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    socket.Send(buffer);
                    Console.Write("Enter a currency 2: ");
                    string message2 = Console.ReadLine();
                    byte[] buffer2 = Encoding.UTF8.GetBytes(message2);
                    socket.Send(buffer2);
                    Console.WriteLine("Client send: " + message + " " + message2);

                    byte[] receiveBuffer = new byte[1024];
                    int received = socket.Receive(receiveBuffer);
                    string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, received);
                    Console.WriteLine("Client received: " + receivedMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
