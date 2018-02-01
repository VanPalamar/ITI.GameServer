using System;
using ITI.GameServer.Messages;
using ITI.GameServer.Models;
using ITIGameServer.Client;
using ITIGameServer.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITIGameServer.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestRunServer() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                var server = new ServerRunner(null, -2);

                server.Start();
            });
        }
        [TestMethod]
        public void TestJoinServer()
        {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            Assert.IsNotNull(server.GetPlayer("player"));
        }
        [TestMethod]
        public void TestPlayerInfo() {
            var initiator = InitiateServer();
            var player = initiator.Item2;
            var position = new MoveInfo() {
                X = player.X,
                Y = player.Y,
                Pseudo = player.Pseudo
            };
            Assert.IsNotNull(position); //A revoir
        }

        [TestMethod]
        public void TestGetPlayerPosition() {
            var player = InitiateServer().Item2;
            var position = new MoveInfo()
            {
                X = player.X,
                Y = player.Y,
                Pseudo = player.Pseudo
            };
            Assert.IsNotNull(position);
        }

        #region Helpers
        /*
         * helper: A revoir si c une bonne idée ou pas
         * retourne un serveur en marche / un joueur / clientServer en marche
         */
        private static (ServerRunner serverRunner, Player playerInit, ClientRunner clientRunner) InitiateServer()
        {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            return (server, server.GetPlayer("player"), client);
        }
        #endregion
         
    }
}
