using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ITI.GameServer.Messages;
using ITI.GameServer.Models;
using ITIGameServer.Messages;
using ITIGameServer.Serialization;

namespace ITIGameServer.Server
{
    public class ServerRunner : IServerRunner
    {
        private bool _isRunning;
        private readonly UdpServer _connection;
        private List<Player> _players;

        public ServerRunner(IPEndPoint endpoint = null, int capacity = 10)
        {
            _players = new List<Player>(capacity);
            _connection = new UdpServer(endpoint ?? new IPEndPoint(IPAddress.Any, 32123));
        }

        public async void Start()
        {
            _isRunning = true;
            while (_isRunning)
            {
                await Loop();
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _connection.Close();
        }

        public void DispatchMessage(Received message)
        {
            switch (message.Message.Type)
            {
                case EMessageType.Connect:
                    LoginInfo connectInfo = Serializer.Deserialize<LoginInfo>(message.Message.Arguments);
                    TryJoinPlayer(connectInfo, message);
                    break;
                case EMessageType.Leave:
                    LoginInfo leaveInfo = Serializer.Deserialize<LoginInfo>(message.Message.Arguments);
                    LeavePlayer(leaveInfo);
                    break;
                case EMessageType.MovePlayer:
                    MoveInfo moveInfo = Serializer.Deserialize<MoveInfo>(message.Message.Arguments);
                    MovePlayer(moveInfo, message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown message type received");
            }
        }

        private async Task Loop()
        {
            Received received = await _connection.Receive();
            DispatchMessage(received);
        }

        public void TryJoinPlayer(LoginInfo info, Received from)
        {
            var isExisting = _players.Any(x => x.Pseudo == info.Pseudo);
            if (isExisting)
            {
                throw new Exception("This player is already connected to the server");
            }
            if (_players.Count == _players.Capacity)
            {
                throw new Exception("Maximum player count reached");
            }
            var player = new Player() {Pseudo = info.Pseudo};
            _players.Add(player);
            _connection.ReplyAsync(new Message(EMessageType.ConnectConfirm), from.Sender);
            Console.WriteLine($"[SERVER] - Player {player.Pseudo} joined [{player.X}, {player.Y}]");
        }

        public void MovePlayer(MoveInfo info, Received @from) {
            throw new NotImplementedException();
        }

        public bool LeavePlayer(LoginInfo info) {
            throw new NotImplementedException();
        }

        public Player GetPlayer(string pseudo) {
            throw new NotImplementedException();
        }

        public MoveInfo GetPlayerPosition(Player p) {
            throw new NotImplementedException();
        }

        public LoginInfo GetPlayerInfo(string pseudo) {
            throw new NotImplementedException();
        }

    }
}
