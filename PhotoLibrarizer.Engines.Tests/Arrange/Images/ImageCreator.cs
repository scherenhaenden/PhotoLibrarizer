using ImageMagick;

namespace PhotoLibrarizer.Engines.Tests.Arrange.Images
{
    public class ImageCreator
    {
        public void CreateTestImage(string path)
        {
            // Create a new image
            using (var image = new MagickImage(MagickColors.White, 100, 100)) // 100x100 white image
            {
                // Here you can modify the image as needed 

                // Save the image
                image.Write(path);
            }
        }
    }
}