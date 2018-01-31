using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace ITI.GameServer.Messages
{
    [MessagePackObject]
    public abstract class BaseMessage
    {
        [Key(0)]
        public string Pseudo { get; set; }
    }
}
