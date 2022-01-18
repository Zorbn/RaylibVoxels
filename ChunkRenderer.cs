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
            GenFaceIfNecessary(x, y, z, Direction.Axis.XPos);
            GenFaceIfNecessary(x, y, z, Direction.Axis.XNeg);
            GenFaceIfNecessary(x, y, z, Direction.Axis.YPos);
            GenFaceIfNecessary(x, y, z, Direction.Axis.YNeg);
            GenFaceIfNecessary(x, y, z, Direction.Axis.ZPos);
            GenFaceIfNecessary(x, y, z, Direction.Axis.ZNeg);
        }

        private int GetBlock(int x, int y, int z)
        {
            if (x < 0 || x >= blocks.GetLength(0)) return 0;
            if (y < 0 || y >= blocks.GetLength(1)) return 0;
            if (z < 0 || z >= blocks.GetLength(2)) return 0;

            return blocks[x, y, z];
        }

        private void GenFaceIfNecessary(int x, int y, int z, Direction.Axis direction)
        {
            Vector3 offset = Direction.AxisToVec(direction);
            
            if (GetBlock(
                x + (int) offset.X,
                y + (int) offset.Y,
                z + (int) offset.Z) != 0) 
                return;
        
            GenFace(x, y, z, direction);
        }

        private void GenFace(int x, int y, int z, Direction.Axis direction)
        {
            Vector3 position = new(x, y, z);

            for (int i = 0; i < CubeMesh.FaceVertices[direction].Length; i++)
            {
                MeshUtils.AddFVec3(vertexComponents, position + CubeMesh.FaceVertices[direction][i]);
                MeshUtils.AddFVec2(texcoordComponents, CubeMesh.FaceTexCoords[direction][i]);
            }
        }
    }
}
