using System.Collections.Generic;
using UnityEngine;
using AleshaCoder.Net;

namespace AleshaCoder.Gameplay
{
    /// Координатор, реагирующий на сетевые события и управляющий спавном.
    public sealed class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerFactory _factory;

        private IRoomService _room;
        private readonly Dictionary<string, RemoteUnitSync> _remotes = new();

        private LocalUnitSync _local;

        public void Init(IRoomService room)
        {
            _room = room;

            _room.OnJoined += _ => OnJoined();
            _room.OnPlayerJoined += OnPlayerJoined;
            _room.OnPlayerLeft += OnPlayerLeft;
            _room.OnPlayerPosition += OnPlayerPosition;
        }

        private void OnJoined()
        {
            _local = _factory.SpawnLocal(Vector3.zero);
            _local.Init(_room);
        }

        private void OnPlayerJoined(string id)
        {
            if (_remotes.ContainsKey(id)) return;
            var ru = _factory.SpawnRemote(id, Vector3.zero);
            _remotes[id] = ru;
        }

        private void OnPlayerLeft(string id)
        {
            if (_remotes.TryGetValue(id, out var ru))
            {
                Destroy(ru.gameObject);
                _remotes.Remove(id);
            }
        }

        private void OnPlayerPosition(string id, Vector3 pos)
        {
            if (id == _room.SessionId) return;
            if (!_remotes.TryGetValue(id, out var ru))
                ru = _remotes[id] = _factory.SpawnRemote(id, pos);
            ru.PushPosition(pos);
        }
    }
}