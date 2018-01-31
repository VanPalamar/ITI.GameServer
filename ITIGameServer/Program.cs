using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ITIGameServer;
using ITIGameServer.Client;
using ITIGameServer.Messages;
using ITIGameServer.Server;

namespace ITIGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a new server
            Task.Factory.StartNew(() =>
            {
                var server = new ServerRunner();

                server.Start();
            });

            Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    var client = new ClientRunner("127.0.0.1", 32123);
                    client.Start();
                    client.JoinServer($"player{i}");
                }
            });
            Console.ReadKey();
        }
    }
}
