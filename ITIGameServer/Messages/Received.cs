using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ITIGameServer.Messages
{
    public struct Received
    {
        public IPEndPoint Sender;
        public Message Message;
    }
}
