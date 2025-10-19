using System.Threading.Tasks;

namespace AleshaCoder.Net
{
    public interface INetworkClient
    {
        Task ConnectAsync(string endpoint);
        bool IsConnected { get; }
    }
}