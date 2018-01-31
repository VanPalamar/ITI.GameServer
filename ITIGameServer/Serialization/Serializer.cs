using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MessagePack;

namespace ITIGameServer.Serialization
{
    public static class Serializer
    {
        public static byte[] Serialize(byte[] toSerialize)
        {
            return MessagePackSerializer.Serialize(toSerialize);
        }

        public static byte[] Serialize(Stream toSerialize)
        {
            return MessagePackSerializer.Serialize(toSerialize);
        }

        public static byte[] Serialize<T>(T toSerialize)
        {
            return MessagePackSerializer.Serialize(toSerialize);
        }

        public static T Deserialize<T>(byte[] input)
        {
            return MessagePackSerializer.Deserialize<T>(input);
        }

        public static T Deserialize<T>(Stream input)
        {
            return MessagePackSerializer.Deserialize<T>(input);
        }
    }
}
