using Raylib_CsLo;

namespace Voxels
{
    public unsafe class ChunkMesh
    {
        public Mesh mesh;
        private List<float> vertices = new List<float>();
        private List<float> texcoords = new List<float>();
        private List<ushort> indices = new List<ushort>();
        
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
            // Bottom vertices
            MeshUtils.AddFVec(vertices, new(x + 0f, y + 0f, z + 0f));
            MeshUtils.AddFVec(vertices, new(x + 0f, y + 0f, z + 1f));
            MeshUtils.AddFVec(vertices, new(x + 1f, y + 0f, z + 0f));
            MeshUtils.AddFVec(vertices, new(x + 1f, y + 0f, z + 1f));

            // Top vertices
            MeshUtils.AddFVec(vertices, new(x + 0f, y + 1f, z + 0f));
            MeshUtils.AddFVec(vertices, new(x + 0f, y + 1f, z + 1f));
            MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z + 0f));
            MeshUtils.AddFVec(vertices, new(x + 1f, y + 1f, z + 1f));

            // Tex coords
            float w = 0.25f;
            float h = 0.33f;

            /*
            MeshUtils.AddTexCoord(texcoords, 2 * w, h);     // 0
            MeshUtils.AddTexCoord(texcoords, 3 * w, h);     // 1
            MeshUtils.AddTexCoord(texcoords, w, h);         // 2
            MeshUtils.AddTexCoord(texcoords, 4 * w, h);     // 3
            MeshUtils.AddTexCoord(texcoords, 2 * w, 2 * h); // 4
            MeshUtils.AddTexCoord(texcoords, 3 * w, 2 * h); // 5
            MeshUtils.AddTexCoord(texcoords, w, 2 * h);     // 6
            MeshUtils.AddTexCoord(texcoords, 4 * w, 2 * h); // 7
            */

            MeshUtils.AddTexCoord(texcoords, 2 * w, h);     // 0
            MeshUtils.AddTexCoord(texcoords, w, h);     // 1
            MeshUtils.AddTexCoord(texcoords, 3 * w, h);         // 2
            MeshUtils.AddTexCoord(texcoords, 0, h);     // 3
            MeshUtils.AddTexCoord(texcoords, 2 * w, 2 * h); // 4
            MeshUtils.AddTexCoord(texcoords, w, 2 * h); // 5
            MeshUtils.AddTexCoord(texcoords, 3 * w, 2 * h);     // 6
            MeshUtils.AddTexCoord(texcoords, 0, 2 * h); // 7


            // Indices
            int vc = (vertices.Count / 3) - 8;

            // Bottom
            MeshUtils.AddUVec(indices, (ushort)(vc + 0), (ushort)(vc + 2), (ushort)(vc + 1));
            MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc + 2), (ushort)(vc + 3));

            // -X
            MeshUtils.AddUVec(indices, (ushort)(vc + 0), (ushort)(vc + 5), (ushort)(vc + 4));
            MeshUtils.AddUVec(indices, (ushort)(vc + 0), (ushort)(vc + 1), (ushort)(vc + 5));

            // +Z
            MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc + 7), (ushort)(vc + 5));
            MeshUtils.AddUVec(indices, (ushort)(vc + 1), (ushort)(vc + 3), (ushort)(vc + 7));

            // -Z
            MeshUtils.AddUVec(indices, (ushort)(vc + 2), (ushort)(vc + 4), (ushort)(vc + 6));
            MeshUtils.AddUVec(indices, (ushort)(vc + 2), (ushort)(vc + 0), (ushort)(vc + 4));

            // +X
            MeshUtils.AddUVec(indices, (ushort)(vc + 3), (ushort)(vc + 6), (ushort)(vc + 7));
            MeshUtils.AddUVec(indices, (ushort)(vc + 3), (ushort)(vc + 2), (ushort)(vc + 6));

            // Top
            MeshUtils.AddUVec(indices, (ushort)(vc + 4), (ushort)(vc + 5), (ushort)(vc + 6));
            MeshUtils.AddUVec(indices, (ushort)(vc + 6), (ushort)(vc + 5), (ushort)(vc + 7));
        }
    }
}
