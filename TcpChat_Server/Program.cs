using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            Console.WriteLine("[SERVER]");

            // socket TCP
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 7632); // Создаём endpoint = 127.0.0.1:7632

            socket.Bind(endPoint); // Привязываем сокет к endpoint

            socket.Listen(1);

            Console.WriteLine("Ожидаем звонка от клиента...");


            Socket socket_client = socket.Accept();


            Console.WriteLine("Клиент на связи");

            while (true)
            {
                byte[] bytes = new byte[1024];
                int num_bytes = socket_client.Receive(bytes);
                string textFromClient = Encoding.Unicode.GetString(bytes, 0, num_bytes);
                Console.WriteLine(textFromClient);

                string message = Console.ReadLine();
                byte[] bytess = Encoding.Unicode.GetBytes(message);
                socket_client.Send(bytess);

            }
            Console.ReadLine();
        }
    }
}
