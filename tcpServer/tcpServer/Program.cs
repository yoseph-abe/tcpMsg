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
            Console.WriteLine("Server started...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] hello = new byte[1024];
                hello = Encoding.Default.GetBytes("Server Recived!");

                stream.Write(hello, 0, hello.Length);

                if (client.Connected)
                {
                    string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    Console.WriteLine("Recived Message From: " + clientIP);
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }

            }
        }

        static void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] clientMsg = new byte[1024];

            try
            {
                
                stream.Read(clientMsg, 0, clientMsg.Length);
                Console.WriteLine(Encoding.Default.GetString(clientMsg));
                stream.Flush();
                //Array.Clear(clientMsg, 0, clientMsg.Length);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client Disconnected!");
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
    }
}