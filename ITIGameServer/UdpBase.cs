using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ITIGameServer.Messages;
using ITIGameServer.Serialization;

namespace ITIGameServer
{
    abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<Received> Receive()
        {
            var result = await Client.ReceiveAsync();
            Message message = Serializer.Deserialize<Message>(result.Buffer);
            return new Received()
            {
                Message = message,
                Sender = result.RemoteEndPoint
            };
        }
    }
}
