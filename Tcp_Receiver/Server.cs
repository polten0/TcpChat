using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpChat_Library;
using Newtonsoft.Json;
using TcpChat_Library.Models;

namespace Tcp_Receiver
{
    public class Server
    {
        private Socket socket;
        private bool isConnected = false;
        
        public bool IsConnected { get => isConnected; set => isConnected = value; }
        public int X { get; set; }
        public int Y { get; set; }

        public Server()
        {
            // socket TCP
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse("127.0.0.1");

            // создаем endpoint = 127.0.0.1:7632
            IPEndPoint endPoint = new IPEndPoint(address, 7632);

            // привязываем сокет к endpoint
            socket.Bind(endPoint);

            socket.Listen(2);   // переводим сокет в режим СЛУШАТЬ
        }

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        public void Run()
        {
            Socket socket_client = socket.Accept();

            isConnected = true;

            while (true)
            {
                string text = Utility.ReceiveMessage(socket_client);

                Hero hero = JsonConvert.DeserializeObject<Hero>(text);

                X = hero.X;
                Y = hero.Y;
            }
        }
    }
}
