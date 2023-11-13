using ImageMagick;

namespace PhotoLibrarizer.Engines.WaterMarking;

public class WaterMarker : IWaterMarker
{
    public void AddWatermark(string sourceFilePath, string outputFilePath, string watermarkFilePath)
    {
        using (MagickImage sourceImage = new MagickImage(sourceFilePath))
        using (MagickImage watermarkImage = new MagickImage(watermarkFilePath))
        {
            // Determine the watermark size relative to the source image size
            int watermarkWidth = (int)(sourceImage.Width * 0.2); // You can adjust the watermark size as needed
            int watermarkHeight = (watermarkWidth * watermarkImage.Height) / watermarkImage.Width;

            watermarkImage.Resize(watermarkWidth, watermarkHeight);

            // Set the position of the watermark (bottom-right corner in this case)
            int posX = sourceImage.Width - watermarkWidth - 10; // Adjust the values to position the watermark
            int posY = sourceImage.Height - watermarkHeight - 10;

            // Composite the watermark onto the source image
            sourceImage.Composite(watermarkImage, posX, posY, CompositeOperator.Over);

            // Save the watermarked image
            sourceImage.Write(outputFilePath);
        }
    }



    public Stream AddWatermarkAndGetStream(Stream sourceImageStream, Stream watermarkImageStream)
    {
        using (MagickImage sourceImage = new MagickImage(sourceImageStream))
        using (MagickImage watermarkImage = new MagickImage(watermarkImageStream))
        {
            // Determine the watermark size relative to the source image size
            int watermarkWidth = (int)(sourceImage.Width * 0.2); // You can adjust the watermark size as needed
            int watermarkHeight = (watermarkWidth * watermarkImage.Height) / watermarkImage.Width;

            watermarkImage.Resize(watermarkWidth, watermarkHeight);

            // Set the position of the watermark (bottom-right corner in this case)
            int posX = sourceImage.Width - watermarkWidth - 10; // Adjust the values to position the watermark
            int posY = sourceImage.Height - watermarkHeight - 10;

            // Composite the watermark onto the source image
            sourceImage.Composite(watermarkImage, posX, posY, CompositeOperator.Over);

            // Create a MemoryStream to store the watermarked image
            using (MemoryStream resultStream = new MemoryStream())
            {
                // Save the watermarked image to the result stream
                sourceImage.Write(resultStream, MagickFormat.Png);
                    
                // Reset the result stream position to the beginning
                resultStream.Seek(0, SeekOrigin.Begin);
                    
                // Return the result stream
                return resultStream;
            }
        }
    }

   public Stream AddWatermarkAndGetStream(Stream sourceImageStream, string watermarkFilePath)
    {
        // Ensure the input stream's position is at the beginning
        sourceImageStream.Seek(0, SeekOrigin.Begin);
        
        // Create a MemoryStream to store the source image
        using (var sourceMemoryStream = new MemoryStream())
        {
            // Copy the content of the sourceImageStream to the sourceMemoryStream
            sourceImageStream.CopyTo(sourceMemoryStream);

            // Create a MemoryStream for the watermark image
            using (var watermarkMemoryStream = new MemoryStream())
            using (MagickImage watermarkImage = new MagickImage(watermarkFilePath))
            using (MagickImage sourceImage = new MagickImage(sourceMemoryStream.ToArray()))
            {
                // Determine the watermark size relative to the source image size
                int watermarkWidth = (int)(sourceImage.Width * 0.2); // You can adjust the watermark size as needed
                int watermarkHeight = (watermarkWidth * watermarkImage.Height) / watermarkImage.Width;

                watermarkImage.Resize(watermarkWidth, watermarkHeight);

                // Set the position of the watermark (bottom-right corner in this case)
                int posX = sourceImage.Width - watermarkWidth - 10; // Adjust the values to position the watermark
                int posY = sourceImage.Height - watermarkHeight - 10;

                // Composite the watermark onto the source image
                sourceImage.Composite(watermarkImage, posX, posY, CompositeOperator.Over);

                // Create a new MemoryStream to store the watermarked image
                
                MemoryStream resultStream = new MemoryStream();
                
                    // Save the watermarked image to the result stream
                    sourceImage.Write(resultStream, MagickFormat.Png);
                        
                    // Reset the result stream position to the beginning
                    resultStream.Seek(0, SeekOrigin.Begin);
                        
                    // Return the result stream
                    return resultStream;
                
                /*using (MemoryStream resultStream = new MemoryStream())
                {
                    // Save the watermarked image to the result stream
                    sourceImage.Write(resultStream, MagickFormat.Png);
                        
                    // Reset the result stream position to the beginning
                    resultStream.Seek(0, SeekOrigin.Begin);
                        
                    // Return the result stream
                    return resultStream;
                }*/
            }
        }
    }


}