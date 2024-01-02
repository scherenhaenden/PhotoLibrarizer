using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Engines.Resizing;
using PhotoLibrarizer.Engines.WaterMarking;

namespace PhotoLibrarizer.Engines.Tests.IntegrationTests
{
    [TestFixture]
    public class SeekStreamsWaterMarkerAndResizeTests
    {
        [Test]
        public void ConvertFileToStream_Resize_Mark_ValidFile_ReturnsStream()
        {
            // Arrange watermark
            string watermarkImagePath = "/Users/edwardflores/Pictures/f60642b79ba667ed85798d85d7bc891c.png";

        
        
            // Arrange seek files
            IFilesSeeker filesSeeker = new FilesSeeker();
            var files = filesSeeker.GetFilesInPath("/Users/edwardflores/Pictures/Rockatuesilo/04/Trivium", new List<string> {".jpg"}, false);
        
            // filer files by using only those from today
            /*DateTime today = DateTime.Today;
            files = files
                .Where(filePath =>
                {
                    DateTime fileDate = File.GetLastWriteTime(filePath); // Modify to use File.GetCreationTime for creation date
                    return fileDate.Date == today;
                })
                .ToList();*/
        
       
            files= files.Where(x=>x.ToLower().Contains("ai")).ToList();
        
            // Act
            foreach (var file in files)
            {
                IFileToStreamConverter converter = new FileToStreamConverter();
                var stream = converter.TryConvertFileToStream(file, out string errorMessage);
            
     
                // Arrange resize
                IImageResizing resizingImages = new ImageResizing();
                var resizedStream = resizingImages.TryConvertImage(stream, 1200, 1200, out var streamSmall, out string errorMessage2);
            
 
                // Arrange watermark
                IWaterMarker waterMarker = new WaterMarker();
                var watermarkedStream = waterMarker.AddWatermarkAndGetStream(streamSmall, watermarkImagePath);
      
                var newFileName = file.Replace(".jpg", "_marked.jpg");
            
                // Arrange save
                watermarkedStream.ToFile(newFileName);
            
                // create directory if it does not exist
                var markedDirectory = Path.Combine(Path.GetDirectoryName(file) ?? string.Empty, "marked");
            
                if (!Directory.Exists(markedDirectory))
                {
                    Directory.CreateDirectory(markedDirectory);
                }
            
                // move file to marked directory
                var newFilePath = Path.Combine(markedDirectory, Path.GetFileName(newFileName));
                File.Move(newFileName, newFilePath);
            
            
            
                // Assert
                Assert.That(stream, Is.Not.Null);
                Assert.That(errorMessage, Is.Empty);
                Assert.That(stream, Is.InstanceOf<Stream>());

                // Clean up (close the stream)
                stream?.Dispose();
            
            }
        
        
        
     

        
        }
    }
}

