using AleshaCoder.Net;
using UnityEngine;

namespace AleshaCoder.Gameplay
{
    public sealed class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _localPrefab;
        [SerializeField] private GameObject _remotePrefab;

        public LocalUnitSync SpawnLocal(Vector3 at)
        {
            var go = Instantiate(_localPrefab, at, Quaternion.identity);
            return go.GetComponent<LocalUnitSync>();
        }

        public RemoteUnitSync SpawnRemote(string id, Vector3 at)
        {
            var go = Instantiate(_remotePrefab, at, Quaternion.identity);
            var ru = go.GetComponent<RemoteUnitSync>();
            ru.SetNetId(id);
            return ru;
        }
    }
}