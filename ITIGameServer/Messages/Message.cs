using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace ITIGameServer.Messages
{
    [MessagePackObject]
    public class Message
    {
        [Key(0)]
        public EMessageType Type { get; set; }
        [Key(1)]
        public byte[] Arguments { get; set; }

        public Message(EMessageType type, byte[] arguments = null)
        {
            Type = type;
            Arguments = arguments;
        }
    }
}
