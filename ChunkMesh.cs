using Raylib_CsLo;

namespace Voxels
{
    // TODO: Seperate chunkMesh into chunk and chunkrenderer
    public unsafe class ChunkMesh
    {
        public Mesh mesh;
        private List<float> vertices = new List<float>();
        private List<float> texcoords = new List<float>();
        private List<ushort> indices = new List<ushort>();

        // Texcoord units
        private const float vfUnit = 0.5f;
        private const float hfUnit = 0.33f;
        
        private const int size = 2;
        private int[,,] blocks = new int[size, size, size];

        public void UpdateMesh()
        {
            vertices.Clear();
            indices.Clear();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        blocks[x, y, z] = Raylib.GetRandomValue(0, 1);
                    }
                }
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (blocks[x, y, z] == 1)
                        {
                            GenCube(x, y, z);
                        }
                    }
                }
            }

            mesh = MeshUtils.MeshFromLists(vertices, indices, texcoords);
        }

        public void GenCube(int x, int y, int z)
        {
            GenFaceIfNecessary(x, y, z, Direction.XPos);
            GenFaceIfNecessary(x, y, z, Direction.XNeg);
            GenFaceIfNecessary(x, y, z, Direction.YPos);
            GenFaceIfNecessary(x, y, z, Direction.YNeg);
            GenFaceIfNecessary(x, y, z, Direction.ZPos);
            GenFaceIfNecessary(x, y, z, Direction.ZNeg);
        }

        private int GetBlock(int x, int y, int z)
        {
            if (x < 0 || x >= blocks.GetLength(0)) return 0;
            if (y < 0 || y >= blocks.GetLength(1)) return 0;
            if (z < 0 || z >= blocks.GetLength(2)) return 0;

            return blocks[x, y, z];
        }

        private void GenFaceIfNecessary(int x, int y, int z, Direction direction)
        {
            switch(direction)
            {
                case Direction.XPos:
                    if (GetBlock(x + 1, y, z) != 0) return;
                    break;
                
                case Direction.XNeg:
                    if (GetBlock(x - 1, y, z) != 0) return;
                    break;

                case Direction.YPos:
                    if (GetBlock(x, y + 1, z) != 0) return;
                    break;

                case Direction.YNeg:
                    if (GetBlock(x, y - 1, z) != 0) return;
                    break;

                case Direction.ZPos:
                    if (GetBlock(x, y, z + 1) != 0) return;
                    break;

                case Direction.ZNeg:
                    if (GetBlock(x, y, z - 1) != 0) return;
                    break;
            }

            GenFace(x, y, z, direction);
        }

        private void GenFace(int x, int y, int z, Direction direction)
        {
            int vc = (vertices.Count / 3);

            switch(direction)
            {
                case Direction.XPos:
                    MeshUtils.AddFVec(vertices, new(x + 1f, y,      z));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y,      z + 1f));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit,  2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoords, hfUnit,      2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit,  vfUnit);
                    MeshUtils.AddTexCoord(texcoords, hfUnit,      vfUnit);

                    MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc + 2), (ushort)(vc + 3));
                    MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc),     (ushort)(vc + 2));
                    break;
                
                case Direction.XNeg:
                    MeshUtils.AddFVec(vertices, new(x, y,      z));
                    MeshUtils.AddFVec(vertices, new(x, y,      z + 1f));
                    MeshUtils.AddFVec(vertices, new(x, y + 1f, z));
                    MeshUtils.AddFVec(vertices, new(x, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoords, 0,      2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoords, hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 0,      vfUnit);
                    MeshUtils.AddTexCoord(texcoords, hfUnit, vfUnit);

                    MeshUtils.AddUVec(indices, (ushort)(vc), (ushort)(vc + 3), (ushort)(vc + 2));
                    MeshUtils.AddUVec(indices, (ushort)(vc), (ushort)(vc + 1), (ushort)(vc + 3));
                    break;

                case Direction.YPos:
                    MeshUtils.AddFVec(vertices, new(x,      y + 1f, z));
                    MeshUtils.AddFVec(vertices, new(x,      y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoords, hfUnit,     0);
                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoords, hfUnit,     vfUnit);

                    MeshUtils.AddUVec(indices, (ushort)(vc),     (ushort)(vc + 1), (ushort)(vc + 2));
                    MeshUtils.AddUVec(indices, (ushort)(vc + 2), (ushort)(vc + 1), (ushort)(vc + 3));
                    break;
                
                case Direction.YNeg:
                    MeshUtils.AddFVec(vertices, new(x,      y, z));
                    MeshUtils.AddFVec(vertices, new(x,      y, z + 1f));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y, z));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y, z + 1f));

                    MeshUtils.AddTexCoord(texcoords, 0,      0);
                    MeshUtils.AddTexCoord(texcoords, hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoords, 0,      vfUnit);
                    MeshUtils.AddTexCoord(texcoords, hfUnit, vfUnit);

                    MeshUtils.AddUVec(indices, (ushort)(vc    ), (ushort)(vc + 2), (ushort)(vc + 1));
                    MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc + 2), (ushort)(vc + 3));
                    break;
                
                case Direction.ZPos:
                    MeshUtils.AddFVec(vertices, new(x,      y,      z + 1f));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y,      z + 1f));
                    MeshUtils.AddFVec(vertices, new(x,      y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 3 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoords, 3 * hfUnit, 0);

                    MeshUtils.AddUVec(indices, (ushort)(vc), (ushort)(vc + 3), (ushort)(vc + 2));
                    MeshUtils.AddUVec(indices, (ushort)(vc), (ushort)(vc + 1), (ushort)(vc + 3));
                    break;
                
                case Direction.ZNeg:
                    MeshUtils.AddFVec(vertices, new(x,      y,      z));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y,      z));
                    MeshUtils.AddFVec(vertices, new(x,      y + 1f, z));
                    MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z));

                    MeshUtils.AddTexCoord(texcoords, 3 * hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 3 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoords, 2 * hfUnit, vfUnit);

                    MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc + 2), (ushort)(vc + 3));
                    MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc),     (ushort)(vc + 2));
                    break;
            }
        }
    }
}
