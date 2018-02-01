using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ITIGameServer.Correction.Messages;
using ITIGameServer.Correction.Serialization;

namespace ITIGameServer.Correction.Server
{
    class UdpServer : UdpBase
    {
        public UdpServer() : this(new IPEndPoint(IPAddress.Any, 32123))
        {
        }

        public UdpServer(IPEndPoint endpoint)
        {
            Client = new UdpClient(endpoint);
        }

        public Task<int> ReplyAsync(Message message, IPEndPoint endpoint)
        {
            var datagram = Serializer.Serialize(message);
            return Client.SendAsync(datagram, datagram.Length, endpoint);
        }

    }
}
