using PhotoLibrarizer.Engines.IoEngines;

namespace PhotoLibrarizer.Engines.Tests.NUnits.IoEngines
{
    [TestFixture]
    public class FileToStreamConverterTests
    {
        private FileToStreamConverter _converter;

        [SetUp]
        public void SetUp()
    {
        _converter = new FileToStreamConverter();
    }

        [Test]
        public void ConvertFileToStream_ValidFile_ReturnsStream()
    {
        var testDirectory = Path.Combine(Path.GetTempPath(), "FileToStreamConverterTests");
        Directory.CreateDirectory(testDirectory);
        
        
        // Arrange
        string filePath = "testfile.txt"; // A file that exists
        filePath = Path.Combine(testDirectory, filePath);
        File.Create(filePath).Close();

        // Act
        Stream? resultStream = _converter.TryConvertFileToStream(filePath, out string errorMessage);

        // Assert
        Assert.That(resultStream, Is.Not.Null);
        Assert.That(errorMessage, Is.Empty);
        Assert.That(resultStream, Is.InstanceOf<Stream>());

        // Clean up (close the stream)
        resultStream?.Dispose();
    }

        [Test]
        public void ConvertFileToStream_InvalidFile_ReturnsNullAndErrorMessage()
    {
        // Arrange
        string filePath = "nonexistentfile.txt"; // A file that does not exist

        // Act
        Stream? resultStream = _converter.TryConvertFileToStream(filePath, out string errorMessage);

        // Assert
        Assert.That(resultStream, Is.Null);
        Assert.That(errorMessage, Is.Not.Empty);

        // Clean up (no need to close a null stream)
    }
    }
}