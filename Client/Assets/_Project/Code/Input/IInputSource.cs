namespace AleshaCoder.Inputs
{
    public interface IInputSource
    {
        /// Нормализованный вектор движения по плоскости (x,z).
        UnityEngine.Vector2 ReadMoveAxis();
        /// Опционально — скорость бега/модификатор
        float ReadSpeedMultiplier() => 1f;
        /// Для последующей масштабируемости — тик ввода
        void Tick(float deltaTime) { }
    }
}