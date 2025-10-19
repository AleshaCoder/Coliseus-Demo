using UnityEngine;

namespace AleshaCoder.Net
{
    [CreateAssetMenu(menuName = "AleshaCoder/Net/Config")]
    public sealed class NetConfig : ScriptableObject
    {
        public string Endpoint = "ws://localhost:2567";
        public string RoomName = "wasd-room";
        public float SendRate = 10f;
        public float RemoteInterpolation = 12f;
        public float MaxExtrapolationMs = 100f;
    }
}