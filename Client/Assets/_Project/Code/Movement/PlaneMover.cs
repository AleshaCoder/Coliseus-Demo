using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Serialization;

namespace AleshaCoder.Movement
{
    /// Отвечает только за физику/кинетику перемещения по XZ.
    public sealed class PlaneMover : MonoBehaviour, IMover
    {
        [SerializeField] private MovementConfig _cfg;
        [SerializeField, MaybeNull] private CharacterController _cc;

        private Vector3 _velocity;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void Move(Vector2 planarDir, float speed, float dt)
        {
            CalculateVelocity(planarDir, speed, dt);
            ApplyPosition(dt);
            ApplyRotation(planarDir, dt);
        }

        private void CalculateVelocity(Vector2 planarDir, float speed, float dt)
        {
            var targetVel = new Vector3(planarDir.x, 0, planarDir.y) * speed;
            _velocity = Vector3.MoveTowards(_velocity, targetVel, _cfg.Acceleration * dt);
        }
        
        private void ApplyPosition(float dt)
        {
            var delta = _velocity * dt;
            if (_cc) _cc.Move(delta);
            else transform.position += delta;
        }

        private void ApplyRotation(Vector2 planarDir, float dt)
        {
            if (!(planarDir.sqrMagnitude > 0.0001f)) return;
            var look = Quaternion.LookRotation(new Vector3(planarDir.x, 0, planarDir.y), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, _cfg.RotationSpeed * dt);
        }
    }
}