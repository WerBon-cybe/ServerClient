using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 13000);
        server.Start();
        Console.WriteLine("Сервер запущен. Ожидание подключения...");
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Подключен клиент: {0}", client.Client.RemoteEndPoint.ToString());
            NetworkStream stream = client.GetStream();
            string currentTime = DateTime.Now.ToString();
            byte[] data = Encoding.ASCII.GetBytes(currentTime);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Получено: {0}", currentTime);
            client.Close();
        }
    }
}
