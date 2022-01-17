using Raylib_CsLo;
using System.Runtime.InteropServices;
using System.Numerics;

namespace Voxels
{
    public unsafe static class MeshUtils
    {
        public static void AllocateMeshData(Mesh* mesh, int triangleCount)
        {
            mesh->vertexCount = triangleCount * 3;
            mesh->triangleCount = triangleCount;

            mesh->vertices = (float*)NativeMemory.Alloc((nuint)(mesh->vertexCount * 3 * sizeof(float)));
            mesh->texcoords = (float*)NativeMemory.Alloc((nuint)(mesh->vertexCount * 2 * sizeof(float)));
            mesh->normals = (float*)NativeMemory.Alloc((nuint)(mesh->vertexCount * 3 * sizeof(float)));
            mesh->indices = (ushort*)NativeMemory.Alloc((nuint)(mesh->vertexCount * sizeof(ushort)));
        }

        public static void AddFVec(List<float> list, Vector3 position)
        {
            list.Add(position.X);
            list.Add(position.Y);
            list.Add(position.Z);
        }

        public static void AddUVec(List<ushort> list, ushort x, ushort y, ushort z)
        {
            list.Add(x);
            list.Add(y);
            list.Add(z);
        }

        public static void AddTexCoord(List<float> list, float x, float y)
        {
            list.Add(x);
            list.Add(y);
        }

        public static Mesh MeshFromLists(List<float> vertices, List<ushort> indices, List<float> texcoords)
        {
            Mesh mesh = new Mesh();
            AllocateMeshData(&mesh, indices.Count / 3);

            fixed (float* verticesPtr = vertices.ToArray())
            {
                mesh.vertices = verticesPtr;
            }

            fixed (ushort* indicesPtr = indices.ToArray())
            {
                mesh.indices = indicesPtr;
            }

            fixed (float* texcoordsPtr = texcoords.ToArray())
            {
                mesh.texcoords = texcoordsPtr;
            }

            Raylib.UploadMesh(&mesh, false);

            return mesh;
        }
    }
}
