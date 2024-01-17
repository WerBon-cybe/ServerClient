using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient();
                Console.Write("Введите IP-адрес удаленной машины: ");
                string server = Console.ReadLine();
                Console.Write("Введите порт: ");
                int port = Convert.ToInt32(Console.ReadLine());
                client.Connect(server, port);

                Console.WriteLine("Подключение к {0} успешно установлено.", client.Client.RemoteEndPoint.ToString());
                NetworkStream stream = client.GetStream();
                byte[] data = System.Text.Encoding.ASCII.GetBytes("GetTime");
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Отправлено: GetTime");
                data = new byte[256];
                string responseData = string.Empty;
                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Отправлено: {0}", responseData);
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Аргументы не могут быть null: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Ошибка сокета: {0}", e);
            }

            Console.WriteLine("\nНажмите Enter для выхода...");
            Console.Read();
        }
    }


}
