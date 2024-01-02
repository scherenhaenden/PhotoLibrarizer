using PhotoLibrarizer.Engines.TopazTransformations.TopazCli;
using PhotoLibrarizer.Engines.WaterMarking;

namespace PhotoLibrarizer.Engines.Tests.NUnits.TopazTransformations.TopazCli
{
    [TestFixture]
    public class CliCommandsTests
    {
        private ICliCommands _cliCommands;
        private WaterMarker _waterMarker;

        [SetUp]
        public void SetUp()
        {
            _cliCommands = new CliCommands();
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
    

    }
}