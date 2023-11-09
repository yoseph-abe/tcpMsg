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
                    Console.WriteLine("Recived from: " + clientIP);
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
                int bytesRead = stream.Read(clientMsg, 0, clientMsg.Length);
                string receivedMessage = Encoding.Default.GetString(clientMsg, 0, bytesRead);
                Console.WriteLine(receivedMessage);

                // Echo the Message received
                byte[] responseBytes = Encoding.UTF8.GetBytes("Received: " + receivedMessage);
                stream.Write(responseBytes, 0, responseBytes.Length);
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