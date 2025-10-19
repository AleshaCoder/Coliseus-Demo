using UnityEngine;
using AleshaCoder.Net;
using AleshaCoder.Gameplay;

namespace AleshaCoder.Composition
{
    public sealed class GameCompositionRoot : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private PlayerPresenter _presenter;

        [Header("Configs")]
        [SerializeField] private NetConfig _net;

        private ColyseusNetworkClient _client;
        private ColyseusRoomService _room;
        private ColyseusUserService _users;

        private async void Start()
        {
            _client = new ColyseusNetworkClient();
            _room   = new ColyseusRoomService(_client);
            _users  = new ColyseusUserService();

            await _users.EnsureUserAsync();
            await _client.ConnectAsync(_net.Endpoint);
            
            _presenter.Init(_room);
            await _room.JoinOrCreateAsync(_net.RoomName);
        }
        
        
        private void OnDestroy()
        {
            _ = _room.LeaveAsync();
        }
    }
}