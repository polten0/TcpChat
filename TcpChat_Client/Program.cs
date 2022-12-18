using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";
            Console.WriteLine("[CLIENT]");

            Socket socket_sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endRemoutePoint = new IPEndPoint(address, 7632);

            Console.WriteLine("Нажмите enter для подключения");
            Console.ReadLine();

            socket_sender.Connect(endRemoutePoint);

            Action<Socket> taskSendMessage = SendMessageForTask;
            IAsyncResult res1 = taskSendMessage.BeginInvoke(socket_sender, null, null);

            Action<Socket> taskReceiveMessage = ReceiveMessageForTask;
            IAsyncResult res2 = taskReceiveMessage.BeginInvoke(socket_sender, null, null);


            taskSendMessage.EndInvoke(res1);
            taskReceiveMessage.EndInvoke(res2);

            Console.ReadLine();
        }

        public static void SendMessage(Socket socket, string message)
        {
            byte[] bytess = Encoding.Unicode.GetBytes(message);
            socket.Send(bytess);
        }
        public static void SendMessageForTask(Socket socket)
        {
            while (true)
            {
                SendMessage(socket, Console.ReadLine());
            }
        }
        public static string ReceiveMessage(Socket socket)
        {
            byte[] bytes = new byte[1024];
            int num_bytes = socket.Receive(bytes);
            return Encoding.Unicode.GetString(bytes, 0, num_bytes);
        }
        public static void ReceiveMessageForTask(Socket socket)
        {
            while (true)
            {
                Console.WriteLine("Server: "+ ReceiveMessage(socket));
            }
        }
    }
}
