using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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


            Thread threadReceive = new Thread(ReceiveMessageForManager);
            threadReceive.Start(socket_client);

            Thread threadSend = new Thread(SendMessageForManager);
            threadSend.Start(socket_client);


            
            Console.ReadLine();
        }



        public static string ReceiveMessage(Socket socket)
        {
            byte[] bytes = new byte[1024];
            int num_bytes = socket.Receive(bytes);
            return Encoding.Unicode.GetString(bytes, 0, num_bytes);
        }
        public static void ReceiveMessageForManager(Object socket)
        {
            while (true)
            {
                Console.WriteLine("Client: " + ReceiveMessage((Socket)socket));
            }
        }


        public static void SendMessage(Socket socket, string message)
        {
            byte[] bytess = Encoding.Unicode.GetBytes(message);
            socket.Send(bytess);
        }
        public static void SendMessageForManager(Object socket)
        {
            while (true)
            {
                SendMessage((Socket)socket, Console.ReadLine());
            }        
        }
    }
}
