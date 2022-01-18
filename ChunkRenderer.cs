using Raylib_CsLo;
using System.Numerics;

namespace Voxels
{
    // TODO: Seperate into chunkdata and chunkrenderer
    // TODO: Use an index system or something to match blocks to textures in the atlas!
    public unsafe class ChunkRenderer
    {
        public Mesh mesh;
        
        private List<float> vertexComponents = new List<float>();
        private List<float> texcoordComponents = new List<float>();

        private const int size = 32;
        private int[,,] blocks = new int[size, size, size];

        public void UpdateMesh()
        {
            vertexComponents.Clear();

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

            mesh = MeshUtils.MeshFromLists(vertexComponents, texcoordComponents);
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
            Vector3 position = new(x, y, z);

            for (int i = 0; i < CubeMesh.FaceVertices[direction].Length; i++)
            {
                MeshUtils.AddFVec3(vertexComponents, position + CubeMesh.FaceVertices[direction][i]);
                MeshUtils.AddFVec2(texcoordComponents, CubeMesh.FaceTexCoords[direction][i]);
            }

            /*
            switch(direction)
            {
                case Direction.XPos:
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y,      z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y,      z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y,      z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z));

                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit,      2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit,  vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit,      vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit,      2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit,  2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit,  vfUnit);
                    break;
                
                case Direction.XNeg:
                    MeshUtils.AddFVec(vertexComponents, new(x, y,      z));
                    MeshUtils.AddFVec(vertexComponents, new(x, y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x, y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x, y,      z));
                    MeshUtils.AddFVec(vertexComponents, new(x, y,      z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoordComponents, 0,      2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 0,      vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 0,      2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, vfUnit);
                    break;

                case Direction.YPos:
                    MeshUtils.AddFVec(vertexComponents, new(x,      y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, vfUnit);
                    break;
                
                case Direction.YNeg:
                    MeshUtils.AddFVec(vertexComponents, new(x,      y, z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y, z));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y, z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y, z + 1f));

                    MeshUtils.AddTexCoord(texcoordComponents, 0, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, 0, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, 0, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, hfUnit, vfUnit);
                    break;
                
                case Direction.ZPos:
                    MeshUtils.AddFVec(vertexComponents, new(x,      y,      z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y + 1f, z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y,      z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y,      z + 1f));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z + 1f));

                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 3 * hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, 0);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 3 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 3 * hfUnit, 0);
                    break;
                
                case Direction.ZNeg:
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y,      z));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y + 1f, z));
                    MeshUtils.AddFVec(vertexComponents, new(x + 1f, y,      z));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y,      z));
                    MeshUtils.AddFVec(vertexComponents, new(x,      y + 1f, z));

                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 3 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 2 * hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 3 * hfUnit, 2 * vfUnit);
                    MeshUtils.AddTexCoord(texcoordComponents, 3 * hfUnit, vfUnit);
                    break;
            }
            */
        }
    }
}
