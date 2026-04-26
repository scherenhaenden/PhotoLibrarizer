using PhotoLibrarizerCore.Services.FilesManagement;
using PhotoLibrarizerCore.Tests.Tests_Helper;

namespace PhotoLibrarizerCore.Tests.Services.FilesManagement
{
    [TestFixture]
    public class IoManagementTests
    {
        private string _testFolderPath;

        [SetUp]
        public void SetUp()
        {
            var currentDirectory = TestsHelper.GetTestsPath();
            // Get files in current directory
            //var filesInCurrentDirectory = Directory.GetFiles(currentDirectory);
        
            // Create directory for IoManagementTests if it doesn't exist
            currentDirectory = Path.Combine(currentDirectory, "IoManagementTests");
            if (Directory.Exists(currentDirectory))
            {
                Directory.Delete(currentDirectory, recursive: true);
            }
            Directory.CreateDirectory(currentDirectory);
        
            _testFolderPath = currentDirectory;

            // Create some test files
            File.Create(Path.Combine(_testFolderPath, "file1.txt")).Close();
            File.Create(Path.Combine(_testFolderPath, "file2.txt")).Close();
            Directory.CreateDirectory(Path.Combine(_testFolderPath, "subfolder"));
            File.Create(Path.Combine(_testFolderPath, "subfolder", "file3.txt")).Close();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the test folder and files
            Directory.Delete(_testFolderPath, recursive: true);
        }

        [Test]
        public void GetFiles_NoExtensions_ReturnsAllFiles()
        {
            // Arrange
            IIoManagement ioManagement = new IoManagement();

            // Act
            var files = ioManagement.GetFiles(_testFolderPath);
            var filesWithSubDirectories = ioManagement.GetFiles(_testFolderPath, true);

            // Assert
            Assert.That(files.Count, Is.EqualTo(2));
            Assert.That(filesWithSubDirectories.Count, Is.EqualTo(3));
            Assert.That(files, Does.Contain(Path.Combine(_testFolderPath, "file1.txt")));
            Assert.That(files, Does.Contain(Path.Combine(_testFolderPath, "file2.txt")));
            Assert.That(filesWithSubDirectories, Does.Contain(Path.Combine(_testFolderPath, "subfolder", "file3.txt")));

        }

        [Test]
        public void GetFiles_WithExtensions_ReturnsFilteredFiles()
        {
            // Arrange
            IIoManagement ioManagement = new IoManagement();
            string[] extensions = { ".txt" };

            // Act
            var files = ioManagement.GetFiles(_testFolderPath, extensions, recursive: true);

            // Assert
            Assert.That(files.Count, Is.EqualTo(3));
            Assert.That(files, Does.Contain(Path.Combine(_testFolderPath, "file1.txt")));
            Assert.That(files, Does.Contain(Path.Combine(_testFolderPath, "file2.txt")));
            Assert.That(files, Does.Contain(Path.Combine(_testFolderPath, "subfolder", "file3.txt")));

        }
    }
}
