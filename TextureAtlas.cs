using Raylib_CsLo;

public static unsafe class TextureAtlas
{
    public static Image MakeAtlas(List<Image> images, int horizontalImages)
    {
        int imageCount = images.Count;

        if (imageCount == 0) throw new Exception("No images supplied when making atlas!");

        Image atlasImage = images[0];

        int verticalImages = (int) MathF.Ceiling((float) imageCount / horizontalImages);
        int imageWidth = images[0].width;
        int imageHeight = images[0].height;

        Raylib.ImageResizeCanvas(&atlasImage, horizontalImages * imageWidth, verticalImages * imageHeight, 0, 0, Raylib.PURPLE);

        for (int i = 0; i < imageCount; i++)
        {
            int imageIndX = imageCount / horizontalImages;
            int imageIndY = imageCount % verticalImages;

            Raylib.ImageDraw(
                &atlasImage,
                images[i],
                new Rectangle(0, 0, imageWidth, imageHeight),
                new Rectangle(imageIndX * imageWidth, imageIndY * imageHeight, imageWidth, imageHeight),
                Raylib.WHITE
            );
        }

        return atlasImage;
    }
}