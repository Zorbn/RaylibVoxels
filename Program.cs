using Raylib_CsLo;

namespace Voxels
{
    public unsafe static class Program
    {
        public static void Main()
        {
            Raylib.InitWindow(1280, 720, "Voxels");
            Raylib.SetTargetFPS(165);

            Texture templateTexture = Raylib.LoadTexture("assets/TemplateBlock.png");
            Camera3D cam = new Camera3D(new(5f, 0.25f, 5f), new(0f, 0f, 0f), new (0f, 1f, 0f), 45f, 0f);
            Raylib.SetCameraMode(cam, CameraMode.CAMERA_FIRST_PERSON);

            ChunkMesh chunkMesh = new ChunkMesh();
            chunkMesh.UpdateMesh();
            Model chunkModel = Raylib.LoadModelFromMesh(chunkMesh.mesh);
            chunkModel.materials[0].maps[(int)Raylib.MATERIAL_MAP_DIFFUSE].texture = templateTexture;

            while (!Raylib.WindowShouldClose())
            {
                // Update
                // float frameTime = Raylib.GetFrameTime();

                Raylib.UpdateCamera(&cam);

                // Draw
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.RAYWHITE);

                Raylib.BeginMode3D(cam);

                Raylib.DrawModel(chunkModel, new(0f, 0f, 0f), 1f, Raylib.WHITE);

                Raylib.DrawSphere(new(3f, 0f, 0f), 0.25f, Raylib.RED);   // X
                Raylib.DrawSphere(new(0f, 3f, 0f), 0.25f, Raylib.GREEN); // Y
                Raylib.DrawSphere(new(0f, 0f, 3f), 0.25f, Raylib.BLUE);  // Z

                Raylib.DrawGrid(10, 1.0);

                Raylib.EndMode3D();

                Raylib.DrawFPS(10, 10);

                Raylib.EndDrawing();
            }

            Raylib.UnloadModel(chunkModel);
            Raylib.CloseWindow();
        }
    }
}
