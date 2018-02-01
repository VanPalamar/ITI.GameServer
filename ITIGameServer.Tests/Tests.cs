using System;
using System.Threading;
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
            Action a = () => new ServerRunner(null, -2);
            a.ShouldThrow<ArgumentOutOfRangeException>();
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
            server.Stop();
        }

        [TestMethod]
        public void TestGetPlayerPosition() {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            Thread.Sleep(1000);
            var player = server.GetPlayer("player");
            Thread.Sleep(1000);
            server.Invoking(o => o.GetPlayerPosition(player)).ShouldNotThrow<NullReferenceException>();
            server.Stop();
        }

        [TestMethod]
        public void TestLeavePlayer()
        {
            var server = new ServerRunner();
            server.Start();
            var client = new ClientRunner("127.0.0.1", 32123);
            client.Start();
            client.JoinServer("player");
            Thread.Sleep(1000);
            var player = server.GetPlayer("player");
            server.LeavePlayer(server.GetPlayerInfo(player.Pseudo)).Should().Be(true);
            server.Stop();
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
