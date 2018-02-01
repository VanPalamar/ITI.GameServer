using System.Threading.Tasks;
using ITIGameServer.Correction.Messages;

namespace ITIGameServer.Correction
{
    public interface IRunner
    {
        void Start();
        void Stop();
        void DispatchMessage(Received message);
    }
}
