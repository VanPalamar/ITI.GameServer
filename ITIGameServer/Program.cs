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
            var server = new ServerRunner();
            server.Start();

            //create a new client

            var random = new Random();
            for (var i = 0; i < 1; i++)
            {
                var client = new ClientRunner("127.0.0.1", 32123);
                client.Start();
                client.JoinServer("valbg" + random.NextDouble().ToString());
            }
            //client.Stop();

            Console.ReadKey();
        }
    }
}
