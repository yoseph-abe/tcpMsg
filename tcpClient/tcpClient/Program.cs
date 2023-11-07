using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
public class Program
{
    static void Main(string[] args)
    {
        string serverIP = "192.168.177.34";
        int serverPort = 1984;


        while (true)
        {

            TcpClient client = new TcpClient(serverIP, serverPort);

            NetworkStream stream = client.GetStream();
            try
            {
                Console.Write("msg>");
                string input = Console.ReadLine();
                if(input == "exit")
                {
                    stream.Close();
                    client.Close();
                    break;
                }
                byte[] send = new byte[1024];
                send = Encoding.Default.GetBytes(input);
                stream.Write(send, 0, send.Length);

                byte[] msg = new byte[1024];
                stream.Read(msg, 0, msg.Length);
                Console.WriteLine(Encoding.Default.GetString(msg));
            }
            catch (Exception es)
            {
                Console.WriteLine("Error: " +  es.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
        


    }
}