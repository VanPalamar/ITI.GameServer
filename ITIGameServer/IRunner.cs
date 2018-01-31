using System.Threading.Tasks;
using ITIGameServer.Messages;

namespace ITIGameServer
{
    public interface IRunner
    {
        void Start();
        void Stop();
        void DispatchMessage(Received message);
    }
}
