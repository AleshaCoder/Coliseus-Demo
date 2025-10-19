using System;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using UnityEngine;

namespace AleshaCoder.Net
{
    // ---------- Schema definitions ----------
    [Serializable]
    public class Player : Schema
    {
        [Colyseus.Schema.Type(0, "number")] public float x;
        [Colyseus.Schema.Type(1, "number")] public float y;
        [Colyseus.Schema.Type(2, "number")] public float z;
    }

    [Serializable]
    public class State : Schema
    {
        [Colyseus.Schema.Type(0, "map", typeof(MapSchema<Player>))] public MapSchema<Player> players = new();
    }

    // ---------- Service ----------
    public sealed class ColyseusRoomService : IRoomService
    {
        public event Action<string> OnJoined;
        public event Action<string> OnPlayerJoined;
        public event Action<string> OnPlayerLeft;
        public event Action<string, Vector3> OnPlayerPosition;

        private readonly ColyseusNetworkClient _client;
        private ColyseusRoom<State> _room;

        public string SessionId => _room?.SessionId;

        public ColyseusRoomService(ColyseusNetworkClient client)
        {
            _client = client;
        }

        public async Task JoinOrCreateAsync(string roomName)
        {
            await _client.ConnectAsync(_client.Raw.Endpoint.Uri.ToString());
            _room = await _client.Raw.JoinOrCreate<State>(roomName);

            _room.State.players.OnAdd += (key, player) =>
            {
                if (key == _room.SessionId)
                    return;

                OnPlayerJoined?.Invoke(key);
                OnPlayerPosition?.Invoke(key, new Vector3(player.x, player.y, player.z));

                player.OnChange += _ =>
                {
                    OnPlayerPosition?.Invoke(key, new Vector3(player.x, player.y, player.z));
                };
            };

            _room.State.players.OnRemove += (key, player) =>
            {
                OnPlayerLeft?.Invoke(key);
            };

            OnJoined?.Invoke(_room.SessionId);
        }

        public async Task SendPositionAsync(Vector3 pos, Vector3 vel)
        {
            if (_room == null) return;
            await _room.Send("setPos", new { x = pos.x, y = pos.y, z = pos.z });
        }

        public async Task LeaveAsync()
        {
            if (_room != null)
            {
                await _room.Leave();
                _room = null;
            }
        }
    }
}
