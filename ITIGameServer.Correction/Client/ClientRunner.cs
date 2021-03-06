﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using ITI.GameServer.Messages;
using ITIGameServer.Correction.Messages;
using ITIGameServer.Correction.Serialization;

namespace ITIGameServer.Correction.Client
{
    public class ClientRunner : IRunner
    {
        private bool _isRunning = false;
        private readonly UdpUser _connection;
        public bool Connected { get; private set; }

        public ClientRunner(string hostname, int port)
        {
            _connection = UdpUser.ConnectTo(hostname, port);
        }

        public Task<int> JoinServer(string pseudo)
        {
            return SendMessage(new Message(EMessageType.Connect, Serializer.Serialize(new LoginInfo() {Pseudo = pseudo})));
        }

        public void DispatchMessage(Received message)
        {
            switch (message.Message.Type)
            {
                case EMessageType.ConnectConfirm:
                    Console.WriteLine("[CLIENT] - Connected to server");
                    Connected = true;
                    break;
                default:
                    throw new InvalidOperationException("Unknown message type received");
            }
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

        private async Task Loop()
        {
            try
            {
                var received = await _connection.Receive();
                DispatchMessage(received);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public Task<int> SendMessage(Message message)
        {
            return _connection.SendAsync(message);
        }
    }
}
