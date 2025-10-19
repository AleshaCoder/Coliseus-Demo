using UnityEngine;

namespace AleshaCoder.Net
{
    public abstract class NetworkUnit : MonoBehaviour
    {
        public string NetId { get; private set; }
        public void SetNetId(string id) => NetId = id;
    }
}