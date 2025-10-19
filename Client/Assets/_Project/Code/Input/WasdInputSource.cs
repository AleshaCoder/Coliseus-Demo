using UnityEngine;

namespace AleshaCoder.Inputs
{
    [DisallowMultipleComponent]
    public sealed class WasdInputSource : MonoBehaviour, IInputSource
    {
        [SerializeField] private string _horizontal = "Horizontal";
        [SerializeField] private string _vertical   = "Vertical";
        [SerializeField] private KeyCode _runKey    = KeyCode.LeftShift;
        [SerializeField] private float _runMultiplier = 1.5f;

        public Vector2 ReadMoveAxis()
        {
            return new Vector2(Input.GetAxisRaw(_horizontal), Input.GetAxisRaw(_vertical)).normalized;
        }

        public float ReadSpeedMultiplier() => Input.GetKey(_runKey) ? _runMultiplier : 1f;
    }
}