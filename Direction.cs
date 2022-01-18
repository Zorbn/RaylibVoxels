using System.Numerics;

namespace Voxels
{
    public static class Direction
    {
        public enum Axis
        {
            XPos,
            XNeg,
            YPos,
            YNeg,
            ZPos,
            ZNeg
        }

        public static Vector3 AxisToVec(Axis axis) => axis switch
        {
            Axis.XPos => new(1f, 0f, 0f),
            Axis.XNeg => new(-1f, 0f, 0f),
            Axis.YPos => new(0f, 1f, 0f),
            Axis.YNeg => new(0f, -1f, 0f),
            Axis.ZPos => new(0f, 0f, 1f),
            Axis.ZNeg => new(0f, 0f, -1f),
            _ => throw new ArgumentOutOfRangeException(nameof(axis), $"Cannot convert {axis} to a Vector3!"),
        };
    }
}