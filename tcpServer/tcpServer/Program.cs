using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 1984);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] hello = new byte[1024];
                hello = Encoding.Default.GetBytes("Hello From Server!");

                stream.Write(hello, 0, hello.Length);

                if (client.Connected)
                {
                    byte[] msg = new byte[1024];
                    stream.Read(msg, 0, msg.Length);
                    Console.WriteLine(Encoding.Default.GetString(msg));
                }

            }
        }
    }
}