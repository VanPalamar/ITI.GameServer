using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ITIGameServer.Correction.Messages
{
    public struct Received
    {
        public IPEndPoint Sender;
        public Message Message;
    }
}
