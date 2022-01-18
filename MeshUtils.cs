using Raylib_CsLo;
using System.Runtime.InteropServices;
using System.Numerics;

namespace Voxels
{
    public unsafe static class MeshUtils
    {

        public static void AllocateMeshData(Mesh* mesh, int vertexComponentCount, int texcoordComponentCount)
        {
            mesh->vertexCount = vertexComponentCount / 3;
            mesh->triangleCount = mesh->vertexCount / 3;

            mesh->vertices =  (float*) NativeMemory.Alloc((nuint)(vertexComponentCount * sizeof(float)));
            mesh->texcoords = (float*) NativeMemory.Alloc((nuint)(texcoordComponentCount * sizeof(float)));
        }

        public static void AddFVec3(List<float> list, Vector3 vec)
        {
            list.Add(vec.X);
            list.Add(vec.Y);
            list.Add(vec.Z);
        }

        public static void AddFVec2(List<float> list, Vector2 vec)
        {
            list.Add(vec.X);
            list.Add(vec.Y);
        }

        public static Mesh MeshFromLists(List<float> vertexComponents, List<float> texcoordComponents)
        {
            Mesh mesh = new Mesh();
            AllocateMeshData(&mesh, vertexComponents.Count, texcoordComponents.Count);

            fixed (float* verticesPtr = vertexComponents.ToArray())
            {
                mesh.vertices = verticesPtr;
            }

            fixed (float* texcoordsPtr = texcoordComponents.ToArray())
            {
                mesh.texcoords = texcoordsPtr;
            }

            Raylib.UploadMesh(&mesh, false);

            return mesh;
        }
    }
}
