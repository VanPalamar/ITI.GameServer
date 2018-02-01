using System;
using System.Collections.Generic;
using System.Text;
using ITI.GameServer.Messages;
using ITI.GameServer.Models;
using ITIGameServer.Messages;

namespace ITIGameServer.Server
{
    public interface IServerRunner : IRunner
    {
        void MovePlayer(MoveInfo info, Received from);
        void TryJoinPlayer(LoginInfo info, Received from);
        bool LeavePlayer(LoginInfo info);
        LoginInfo GetPlayerInfo(string pseudo);
        Player GetPlayer(string pseudo);

        MoveInfo GetPlayerPosition(Player p);

    }
}
