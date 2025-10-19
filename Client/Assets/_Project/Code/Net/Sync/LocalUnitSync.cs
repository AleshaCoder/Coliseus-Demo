using System.Threading;
using System.Threading.Tasks;
using AleshaCoder.Movement;
using UnityEngine;

namespace AleshaCoder.Net
{
    /// Локальный юнит: отправляем позицию на сервер с периодом, сервер реплицирует всем.
    public sealed class LocalUnitSync : NetworkUnit
    {
        [SerializeField] private InterfaceReference<IMover> _mover;
        [SerializeField] private NetConfig _config;

        private IRoomService _room;
        private CancellationTokenSource _cts;

        public void Init(IRoomService room)
        {
            _cts = new CancellationTokenSource();
            _room = room;
            RunSendLoop(_cts.Token);
        }

        private void OnDisable()
        {
            _cts?.Cancel();
        }

        private async void RunSendLoop(CancellationToken ct)
        {
            var interval = 1f / Mathf.Max(1, _config.SendRate);
            while (!ct.IsCancellationRequested)
            {
                var pos = _mover.Value.Position;
                await _room.SendPositionAsync(pos, Vector3.zero);
                await Task.Delay(System.TimeSpan.FromSeconds(interval), cancellationToken: ct);
            }
        }
    }
}