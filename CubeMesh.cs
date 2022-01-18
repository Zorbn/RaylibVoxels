using System.Numerics;

namespace Voxels
{
    public static class CubeMesh
    {
        // Texcoord units
        public const float vfUnit = 0.5f;
        public const float hfUnit = 0.33f;

        public static readonly Dictionary<Direction.Axis, Vector3[]> FaceVertices = new()
        {
            {
                Direction.Axis.XPos,
                new Vector3[] {
                    new(1f, 0f, 1f),
                    new(1f, 1f, 0f),
                    new(1f, 1f, 1f),
                    new(1f, 0f, 1f),
                    new(1f, 0f, 0f),
                    new(1f, 1f, 0f),
                }
            },
            {
                Direction.Axis.XNeg,
                new Vector3[] {
                    new(0f, 0f, 0f),
                    new(0f, 1f, 1f),
                    new(0f, 1f, 0f),
                    new(0f, 0f, 0f),
                    new(0f, 0f, 1f),
                    new(0f, 1f, 1f),
                }
            },
            {
                Direction.Axis.YPos,
                new Vector3[] {
                    new(0f, 1f, 0f),
                    new(0f, 1f, 1f),
                    new(1f, 1f, 0f),
                    new(1f, 1f, 0f),
                    new(0f, 1f, 1f),
                    new(1f, 1f, 1f),
                }
            },
            {
                Direction.Axis.YNeg,
                new Vector3[] {
                    new(0f, 0f, 0f),
                    new(1f, 0f, 0f),
                    new(0f, 0f, 1f),
                    new(0f, 0f, 1f),
                    new(1f, 0f, 0f),
                    new(1f, 0f, 1f),
                }
            },
            {
                Direction.Axis.ZPos,
                new Vector3[] {
                    new(0f, 0f, 1f),
                    new(1f, 1f, 1f),
                    new(0f, 1f, 1f),
                    new(0f, 0f, 1f),
                    new(1f, 0f, 1f),
                    new(1f, 1f, 1f),
                }
            },
            {
                Direction.Axis.ZNeg,
                new Vector3[] {
                    new(1f, 0f, 0f),
                    new(0f, 1f, 0f),
                    new(1f, 1f, 0f),
                    new(1f, 0f, 0f),
                    new(0f, 0f, 0f),
                    new(0f, 1f, 0f),
                }
            },
        };

        public static readonly Dictionary<Direction.Axis, Vector2[]> FaceTexCoords = new()
        {
            {
                Direction.Axis.XPos,
                new Vector2[] {
                    new(hfUnit,     2 * vfUnit),
                    new(2 * hfUnit, vfUnit),
                    new(hfUnit,     vfUnit),
                    new(hfUnit,     2 * vfUnit),
                    new(2 * hfUnit, 2 * vfUnit),
                    new(2 * hfUnit, vfUnit),
                }
            },
            {
                Direction.Axis.XNeg,
                new Vector2[] {
                    new(0,      2 * vfUnit),
                    new(hfUnit, vfUnit),
                    new(0,      vfUnit),
                    new(0,      2 * vfUnit),
                    new(hfUnit, 2 * vfUnit),
                    new(hfUnit, vfUnit),
                }
            },
            {
                Direction.Axis.YPos,
                new Vector2[] {
                    new(2 * hfUnit, 0),
                    new(hfUnit,     0),
                    new(2 * hfUnit, vfUnit),
                    new(2 * hfUnit, vfUnit),
                    new(hfUnit,     0),
                    new(hfUnit,     vfUnit),
                }
            },
            {
                Direction.Axis.YNeg,
                new Vector2[] {
                    new(0,      0),
                    new(0,      vfUnit),
                    new(hfUnit, 0),
                    new(hfUnit, 0),
                    new(0,      vfUnit),
                    new(hfUnit, vfUnit),
                }
            },
            {
                Direction.Axis.ZPos,
                new Vector2[] {
                    new(2 * hfUnit, vfUnit),
                    new(3 * hfUnit, 0),
                    new(2 * hfUnit, 0),
                    new(2 * hfUnit, vfUnit),
                    new(3 * hfUnit, vfUnit),
                    new(3 * hfUnit, 0),
                }
            },
            {
                Direction.Axis.ZNeg,
                new Vector2[] {
                    new(2 * hfUnit, 2 * vfUnit),
                    new(3 * hfUnit, vfUnit),
                    new(2 * hfUnit, vfUnit),
                    new(2 * hfUnit, 2 * vfUnit),
                    new(3 * hfUnit, 2 * vfUnit),
                    new(3 * hfUnit, vfUnit),
                }
            },
        };
    }
}