using System;
using FluentAssertions;
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
                
            });
            var server = new ServerRunner(null, -2);

            server.Invoking(o => o.Start()).ShouldThrow<ArgumentOutOfRangeException>();
        }
        [TestMethod]
        public void TestJoinServer()
        {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            server.Invoking(o => o.GetPlayer("player")).Should().NotBeNull();
        }

        [TestMethod]
        public void TestGetPlayerPosition() {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            var player = server.GetPlayer("player");
            server.Invoking(o => o.GetPlayerPosition(player)).ShouldNotThrow<NullReferenceException>();
        }

        [TestMethod]
        public void TestLeavePlayer()
        {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            var player = server.GetPlayer("player");
            server.LeavePlayer(server.GetPlayerInfo(player.Pseudo)).Should().Be(true);
        }


        #region Helper
        /*
         * helper: If you dont like duplicationg code. Use this
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
