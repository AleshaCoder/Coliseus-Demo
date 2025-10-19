using AleshaCoder.Inputs;
using UnityEngine;

namespace AleshaCoder.Movement
{
    /// Соединяет источник ввода и конкретный IMover.
    public sealed class LocalMovementController : MonoBehaviour
    {
        [SerializeField] private MovementConfig _cfg;
        [SerializeField] private InterfaceReference<IMover>  _moverSource;
        [SerializeField] private InterfaceReference<IInputSource> _inputSource;

        private IMover Mover { get; set; }
        private IInputSource Input { get; set; }

        private void Awake()
        {
            Mover = _moverSource.Value;
            Input = _inputSource.Value;
        }

        private void Update()
        {
            var axis = Input.ReadMoveAxis();
            var speed = _cfg.BaseSpeed * Mathf.Max(0.01f, Input.ReadSpeedMultiplier());
            Mover.Move(axis, speed, Time.deltaTime);
        }
    }
}