using PhotoLibrarizerCore.Services.FilesManagement;
using PhotoLibrarizerCore.Services.FilesManagement.Models;
using PhotoLibrarizerCore.Tests.Tests_Helper;

namespace PhotoLibrarizerCore.Tests.Services.FilesManagement
{
    [TestFixture]
    public class FileModelTests
    {
        private FileModel _fileModel;
        private string _fileModelTestsPath;
        private string _fileName1 = "file1.txt";
        private string _fileName2 = "file2.txt";
        private string _fileName3 = "file3.txt";

        [SetUp]
        public void Setup()
    {
        var testsPath = TestsHelper.GetTestsPath();
        var fileModelTestsPath = Path.Combine(testsPath, "FileModelTests");
        //Create directory
        if (Directory.Exists(fileModelTestsPath))
        {
            Directory.Delete(fileModelTestsPath, recursive: true);
        }
        Directory.CreateDirectory(fileModelTestsPath);
        _fileModel = new FileModel();
        _fileModelTestsPath = fileModelTestsPath;
        
        File.Create(Path.Combine(fileModelTestsPath, "file1.txt")).Close();
        File.Create(Path.Combine(fileModelTestsPath, "file2.txt")).Close();
        Directory.CreateDirectory(Path.Combine(fileModelTestsPath, "subfolder"));
        File.Create(Path.Combine(fileModelTestsPath, "subfolder", "file3.txt")).Close();
    }
    
        [TearDown]
        public void TearDown()
    {
        // Clean up the test folder and files
        Directory.Delete(_fileModelTestsPath, recursive: true);
    }


        [Test]
        public void FullPathOfFile_SetValue_PropertiesUpdated()
    {
        
        IIoManagement ioManagement = new IoManagement();
        
        var files = ioManagement.GetFiles(_fileModelTestsPath, true);
        
        // Arrange
        string fullPath = _fileModelTestsPath;

        // Act
        _fileModel.FullPathOfFile = fullPath;

        // Assert
        Assert.AreEqual(fullPath, _fileModel.FullPathOfFile);
        Assert.AreEqual(_fileName1, _fileModel.FileName);
        Assert.AreEqual("C:\\path\\to", _fileModel.PathOfFile);
        Assert.IsNotNull(_fileModel.GeneralFileInformation);
    }

        [Test]
        public void FullPathOfFile_SetValue_FileInfoDateUpdated()
    {
        // Arrange
        string fullPath = "C:\\path\\to\\file.txt";

        // Act
        _fileModel.FullPathOfFile = fullPath;

        // Assert
        DateTime expectedDate = File.GetCreationTime(fullPath);
        Assert.AreEqual(expectedDate, _fileModel.GeneralFileInformation.CreationTime);
    }

        [Test]
        public void DateCreation_SetValue_PropertyUpdated()
    {
        // Arrange
        DateTime date = new DateTime(2022, 1, 1);

        // Act
        _fileModel.DateCreation = date;

        // Assert
        Assert.AreEqual(date, _fileModel.DateCreation);
    }

        [Test]
        public void Hash_SetValue_PropertyUpdated()
    {
        // Arrange
        string hash = "123456789";

        // Act
        _fileModel.Hash = hash;

        // Assert
        Assert.AreEqual(hash, _fileModel.Hash);
    }
    }
}