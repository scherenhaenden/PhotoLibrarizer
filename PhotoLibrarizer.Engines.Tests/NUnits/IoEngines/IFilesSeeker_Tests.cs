using PhotoLibrarizer.Engines.IoEngines;

namespace PhotoLibrarizer.Engines.Tests.NUnits.IoEngines
{
    [TestFixture]
    public class FilesSeekerTests
    {
        /*private Mock<IFilesSeeker> mockFilesSeeker;

        [SetUp]
        public void Setup()
        {
            mockFilesSeeker = new Mock<IFilesSeeker>();
        }

        [Test]
        public void GetFilesInPath_ReturnsListOfFiles()
        {
            // Arrange
            string path = "/path/to/files";
            List<string> expectedFiles = new List<string> { "file1.txt", "file2.jpg", "file3.png" };
            mockFilesSeeker.Setup(f => f.GetFilesInPath(path)).Returns(expectedFiles);

            // Act
            List<string> actualFiles = mockFilesSeeker.Object.GetFilesInPath(path);

            // Assert
            Assert.AreEqual(expectedFiles, actualFiles);
        }

        [Test]
        public void GetFilesInPath_WithExtensions_ReturnsListOfFilesWithSpecifiedExtensions()
        {
            // Arrange
            string path = "/path/to/files";
            List<string> extensions = new List<string> { ".txt", ".jpg" };
            List<string> expectedFiles = new List<string> { "file1.txt", "file2.jpg" };
            mockFilesSeeker.Setup(f => f.GetFilesInPath(path, extensions, false)).Returns(expectedFiles);

            // Act
            List<string> actualFiles = mockFilesSeeker.Object.GetFilesInPath(path, extensions, false);

            // Assert
            Assert.AreEqual(expectedFiles, actualFiles);
        }*/
    
        private string testDirectory;
        private FilesSeeker _filesSeeker;

        [SetUp]
        public void Setup()
    {
        // Create a temporary directory for testing
        testDirectory = Path.Combine(Path.GetTempPath(), "FileSeekerTests");
        Directory.CreateDirectory(testDirectory);

        // Create temporary files for testing
        File.Create(Path.Combine(testDirectory, "file1.txt")).Close();
        File.Create(Path.Combine(testDirectory, "file2.jpg")).Close();
        File.Create(Path.Combine(testDirectory, "file3.png")).Close();

        _filesSeeker = new FilesSeeker();
    }

        [TearDown]
        public void TearDown()
    {
        // Delete the temporary directory and its files
        Directory.Delete(testDirectory, true);
    }

        [Test]
        public void GetFilesInPath_ReturnsListOfFiles()
    {
        // Arrange
        string path = testDirectory;

        // Act
        List<string> files = _filesSeeker.GetFilesInPath(path);

        // Assert
        Assert.IsNotNull(files);
        // Add more assertions as needed
    }

        [Test]
        public void GetFilesInPath_WithExtensions_ReturnsListOfFilesWithSpecifiedExtensions()
    {
        // Arrange
        string path = testDirectory;
        List<string> extensions = new List<string> { ".txt", ".jpg" };

        // Act
        List<string> files = _filesSeeker.GetFilesInPath(path, extensions, false);

        // Assert
        Assert.IsNotNull(files);
        // Add more assertions as needed
    }
        [Test]
        public void GetFilesInPath_CaseSensitiveTrue_ReturnsListOfFiles_CaseSensitive()
    {
        // Arrange
        string path = testDirectory;
        List<string> extensions = new List<string> { ".txt", ".jpg" };

        // Act
        List<string> files = _filesSeeker.GetFilesInPath(path, extensions, true);

        // Assert
        Assert.IsNotNull(files);
        // Add more assertions as needed
    }

        [Test]
        public void GetFilesInPath_CaseSensitiveTrue_ReturnsEmptyList_WhenNoFilesMatch()
    {
        // Arrange
        string path = testDirectory;
        List<string> extensions = new List<string> { ".TXT", ".JPG" }; // Different case extensions

        // Act
        List<string> files = _filesSeeker.GetFilesInPath(path, extensions, true);

        // Assert
        Assert.IsEmpty(files);
        // Add more assertions as needed
    }
    }
}