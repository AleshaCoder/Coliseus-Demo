using UnityEngine;

namespace AleshaCoder.Movement
{
    [CreateAssetMenu(menuName = "AleshaCoder/Movement/Config")]
    public sealed class MovementConfig : ScriptableObject
    {
        public float BaseSpeed = 5f;
        public float Acceleration = 20f;
        public float RotationSpeed = 720f; // deg/s
        public float LocalSmoothing = 12f; // сглаживание локального движения (не сетевое)
    }
}