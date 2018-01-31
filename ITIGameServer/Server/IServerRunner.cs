using System;
using System.Collections.Generic;
using System.Text;
using ITI.GameServer.Messages;
using ITIGameServer.Messages;

namespace ITIGameServer.Server
{
    public interface IServerRunner : IRunner
    {
        void MovePlayer(MoveInfo info, Received from);
        void TryJoinPlayer(LoginInfo info, Received from);
        void LeavePlayer(LoginInfo info, Received from);
    }
}
