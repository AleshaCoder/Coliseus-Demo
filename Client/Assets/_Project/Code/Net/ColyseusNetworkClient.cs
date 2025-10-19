using System.Threading.Tasks;
using Colyseus;

namespace AleshaCoder.Net
{
    public sealed class ColyseusNetworkClient : INetworkClient
    {
        private ColyseusClient _client;
        public bool IsConnected => _client != null;

        public async Task ConnectAsync(string endpoint)
        {
            _client ??= new ColyseusClient(endpoint);
            await Task.CompletedTask;
        }

        public ColyseusClient Raw => _client;
    }
}