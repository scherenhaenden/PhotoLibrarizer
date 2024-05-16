using PhotoLibrarizer.Engines.WaterMarking;

namespace PhotoLibrarizer.Engines.Tests.NUnits.WaterMarking
{
    [TestFixture]
    public class WaterMarkerTests
    {
        private IWaterMarker _waterMarker;

        [SetUp]
        public void SetUp()
    {
        _waterMarker = new WaterMarker();
    }

        [Test]
        public void AddWatermark_WatermarkApplied_Success()
    {
        // Arrange
        string sourceImagePath = "/Users/edwardflores/Pictures/organized/2023/08/02/converted/2023_08_02_22_10_37_32473905.jpg";
        string outputImagePath = "/Users/edwardflores/Pictures/organized/2023/08/02/converted/2023_08_02_22_10_37_32473905_marked.jpg";
        string watermarkImagePath = "/Users/edwardflores/Pictures/f60642b79ba667ed85798d85d7bc891c.png";

        // Act
        _waterMarker.AddWatermark(sourceImagePath, outputImagePath, watermarkImagePath);

        // Assert
        Assert.That(File.Exists(outputImagePath), Is.True, "Watermarked image file should be created.");
    }
    
        [Test]
        public void AddWatermark_WatermarkApplied_Many_Success()
    {
        // Arrange
        // get files from directory
        var files = Directory.GetFiles("/Users/edwardflores/Pictures/organized/2023/08/03/converted/Uriah Heep", "*sharpen*.jpg");
        
        string watermarkImagePath = "/Users/edwardflores/Pictures/f60642b79ba667ed85798d85d7bc891c.png";
        
        // add prefix to file name
        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            var newFileName = fileInfo.Name.Replace(".jpg", "_marked.jpg");
            var newFilePath = Path.Combine(fileInfo.DirectoryName, newFileName);
            
            // Act
            _waterMarker.AddWatermark(file, newFilePath, watermarkImagePath);
            Assert.That(File.Exists(newFilePath), Is.True, "Watermarked image file should be created.");

            
        }
    }
    }
}