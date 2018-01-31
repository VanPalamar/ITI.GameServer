using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITIGameServer.Messages;
using ITIGameServer.Serialization;

namespace ITIGameServer.Client
{
    class UdpUser : UdpBase
    {
        public string Hostname { get; }
        public int Port { get; }

        private UdpUser(string hostname, int port)
        {
            Hostname = hostname;
            Port = port;
        }

        public static UdpUser ConnectTo(string hostname, int port)
        {
            var connection = new UdpUser(hostname, port);
            connection.Client.Client.Connect(hostname, port);
            return connection;
        }

        public Task<int> SendAsync(Message message)
        {
            var datagram = Serializer.Serialize(message);
            return Client.SendAsync(datagram, datagram.Length, Hostname, Port);
        }
    }
}
