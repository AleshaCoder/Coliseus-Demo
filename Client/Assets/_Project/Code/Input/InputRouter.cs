using UnityEngine;

namespace AleshaCoder.Inputs
{
    /// Позволяет позже “горячо” подменять источники ввода (геймпад, клики и т. п.)
    public sealed class InputRouter : MonoBehaviour, IInputSource
    {
        [SerializeField] private InterfaceReference<IInputSource> _source;
        private IInputSource InputSource { get; set; }
        
        private void Awake() => InputSource = _source.Value;

        public Vector2 ReadMoveAxis() => InputSource.ReadMoveAxis();
        public float ReadSpeedMultiplier() => InputSource.ReadSpeedMultiplier();
        public void Tick(float dt) => InputSource.Tick(dt);

        public void SetSource(IInputSource source) => InputSource = source;
    }
}