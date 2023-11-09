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
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            try
            {
                Console.Write("msg>");
                string input = Console.ReadLine();
                if(input == "exit")
                {
                    stream.Close();
                    client.Close();
                    reader.Close();
                    writer.Close();
                    Console.WriteLine("Connection Closed!");
                    break;
                }

                //write message
                writer.WriteLine(input);
                writer.Flush();

                //read server response
                string serverResponse1 = reader.ReadLine();
                Console.WriteLine(serverResponse1);
            }
            catch (Exception es)
            {
                Console.WriteLine("Error: " +  es.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
                reader.Close();
                writer.Close();
            }
        }
    }
}