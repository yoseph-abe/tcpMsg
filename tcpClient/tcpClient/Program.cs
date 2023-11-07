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

        TcpClient client = new TcpClient(serverIP, serverPort);

        NetworkStream stream = client.GetStream();

        Console.Write("msg>");
        string userName = Console.ReadLine();

        byte[] send = new byte[1024];
        send = Encoding.Default.GetBytes(userName);
        stream.Write(send, 0, send.Length);

        byte[] msg = new byte[1024];
        stream.Read(msg, 0, msg.Length);
        Console.WriteLine(Encoding.Default.GetString(msg));

        stream.Close();
        client.Close();
    }
}