using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private ConcurrentDictionary<string, Player> _players;

        public ServerRunner(IPEndPoint endpoint = null, int capacity = 10)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException("Capacity must be grater than 0");
            }
            _players = new ConcurrentDictionary<string, Player>();
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
                    LeavePlayer(leaveInfo, message);
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

        public void MovePlayer(MoveInfo info, Received from)
        {
            throw new NotImplementedException();
        }

        public void TryJoinPlayer(LoginInfo info, Received from)
        {
            var isExisting = _players.TryGetValue(info.Pseudo, out Player otherPlayer);
            if (isExisting)
            {
                throw new Exception("This player is already connected to the server");
            }
            var player = new Player() {Pseudo = info.Pseudo};
            _players.AddOrUpdate(info.Pseudo, player, (key, v) => player);
            _connection.ReplyAsync(new Message(EMessageType.ConnectConfirm), from.Sender);
            Console.WriteLine($"[SERVER] - Player {player.Pseudo} joined [{player.X}, {player.Y}]");
        }

        public void LeavePlayer(LoginInfo info, Received from)
        {
            throw new NotImplementedException();
        }
    }
}
