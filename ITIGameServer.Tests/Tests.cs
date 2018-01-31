using System;
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
            client.JoinServer("val");
            var player = server.GetPlayer("val");
            Assert.IsNotNull(player);
        }
    }
}
