using System.Collections.Generic;
using UnityEngine;

namespace AleshaCoder.Net
{
    /// Удалённый юнит: сглаживаем поступающие с сервера позиции.
    public sealed class RemoteUnitSync : NetworkUnit
    {
        [SerializeField] private float _interp = 12f;
        [SerializeField] private float _snapDist = 3f;

        private readonly Queue<Vector3> _buffer = new();
        private Vector3 _target;

        public void PushPosition(Vector3 p)
        {
            _buffer.Enqueue(p);
            if (_buffer.Count > 3) _buffer.Dequeue();
        }

        private void Update()
        {
            if (_buffer.Count > 0) _target = _buffer.Peek();

            var pos = transform.position;
            var dist = Vector3.Distance(pos, _target);
            if (dist > _snapDist)
            {
                transform.position = _target;
                _buffer.Clear();
            }
            else
            {
                transform.position = Vector3.Lerp(pos, _target, 1f - Mathf.Exp(-_interp * Time.deltaTime));
                if (Vector3.Distance(transform.position, _target) < 0.02f && _buffer.Count > 0)
                    _buffer.Dequeue();
            }
        }
    }
}