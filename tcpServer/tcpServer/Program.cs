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
            string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            try
            {
                string receivedMessage = reader.ReadLine();
                Console.WriteLine(receivedMessage);
                writer.WriteLine("Received: " + receivedMessage);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client Disconnected!");
            }
            finally
            {
                reader.Close();
                writer.Close();
                stream.Close();
                client.Close();
            }
        }
    }
}