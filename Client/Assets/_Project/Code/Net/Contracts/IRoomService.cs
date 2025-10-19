using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AleshaCoder.Net
{
    public interface IRoomService
    {
        event Action<string> OnJoined;
        event Action<string> OnPlayerJoined;
        event Action<string> OnPlayerLeft;
        event Action<string, Vector3> OnPlayerPosition;

        Task JoinOrCreateAsync(string roomName);
        Task SendPositionAsync(Vector3 pos, Vector3 vel);
        Task LeaveAsync();
        string SessionId { get; }
    }
}