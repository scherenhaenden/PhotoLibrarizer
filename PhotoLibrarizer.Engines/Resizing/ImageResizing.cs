using ImageMagick;

namespace PhotoLibrarizer.Engines.Resizing;

public class ImageResizing: IImageResizing
{
    public bool TryConvertImage(string path,  int width, int height, string newPath = "")
    {
        using (var image = new MagickImage(path))
        {
            image.Resize(width, height);
            //image.Quality = 10; // This is the Compression level.
            image.Write(newPath);
            return true;
        }
    }

    public bool TryConvertImage(Stream input, int width, int height, out Stream? output, out string errorMessage)
    {
        try
        {
            // Ensure the input stream's position is at the beginning
            input.Seek(0, SeekOrigin.Begin);
            
            using (var image = new MagickImage(input))
            {
                image.Resize(width, height);

                // Create a new MemoryStream for the output
                var memoryStream = new MemoryStream();

                // You can set image quality or other options here if needed.
                image.Write(memoryStream, MagickFormat.Jpg); // Specify the output format if necessary.

                errorMessage = "";

                // Reset the memory stream position to the beginning
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Create a new MemoryStream from the bytes
                output = new MemoryStream(memoryStream.ToArray());

                return true;
            }
        }
        catch (Exception ex)
        {
            output = null;
            errorMessage = "Error resizing image: " + ex.Message;
            return false;
        }
    }
}