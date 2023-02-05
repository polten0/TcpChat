using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpChat_Library;
using TcpChat_Library.Models;

namespace Tcp_Controler
{
    public class Client
    {
        Hero hero = new Hero();
        Socket socket_sender;

        public Client()
        {
            // GAME
            hero.Weapon = new Weapon();
            hero.Items = new Item[]
            {
                new Item() { Name = "gbhjfe", Description = "1"},
                new Item() { Name = "gfbrve", Description = "2"},
                new Item() { Name = "iuytr", Description = "3"},
            };

            // SETI
            socket_sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse("127.0.0.1");  // server ip
            IPEndPoint endRemoutePoint = new IPEndPoint(address, 7632);   // server port

            // подключаемся к удаленной точке
            socket_sender.Connect(endRemoutePoint);   // lock
        }

        public void MoveRight()
        {
            hero.X += 50;

            Utility.JsonSerializeAndSend(hero, socket_sender);
        }
        public void MoveLeft()
        {
            hero.X -= 50;

            Utility.JsonSerializeAndSend(hero, socket_sender);
        }
        public void MoveUp()
        {
            hero.Y -= 50;

            Utility.JsonSerializeAndSend(hero, socket_sender);
        }
        public void MoveDown()
        {
            hero.Y += 50;

            Utility.JsonSerializeAndSend(hero, socket_sender);
        }
    }
}
