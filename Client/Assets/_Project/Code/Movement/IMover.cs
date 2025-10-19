namespace AleshaCoder.Movement
{
    public interface IMover
    {
        void Move(UnityEngine.Vector2 planarDir, float speed, float dt);
        UnityEngine.Vector3 Position { get; }
        UnityEngine.Quaternion Rotation { get; }
    }
}