using PhotoLibrarizerCore.Services.FilesManagement;
using PhotoLibrarizerCore.Services.FilesManagement.Models;
using PhotoLibrarizerCore.Tests.Tests_Helper;

namespace PhotoLibrarizerCore.Tests.Services.FilesManagement
{
    [TestFixture]
    public class FilesToModelsMapperTests
    {
        private IFilesToModelsMapper _filesToModelsMapper;
        private string _fileModelTestsPath;

        [SetUp]
        public void Setup()
        {
            // Create an instance of the class that implements IFilesToModelsMapper
            _filesToModelsMapper = new GenerateFiles();
            var testsPath = TestsHelper.GetTestsPath();
            var fileModelTestsPath = Path.Combine(testsPath, "FilesToModelsMapperTestsPath");
            _fileModelTestsPath = fileModelTestsPath;
            //Create directory
            if (Directory.Exists(fileModelTestsPath))
            {
                Directory.Delete(fileModelTestsPath, recursive: true);
            }
            Directory.CreateDirectory(fileModelTestsPath);
            //_fileModel = new FileModel();
            _fileModelTestsPath = fileModelTestsPath;
        
            File.Create(Path.Combine(fileModelTestsPath, "file1.txt")).Close();
            File.Create(Path.Combine(fileModelTestsPath, "file2.txt")).Close();
            Directory.CreateDirectory(Path.Combine(fileModelTestsPath, "subfolder"));
            File.Create(Path.Combine(fileModelTestsPath, "subfolder", "file3.txt")).Close();
        }

        [Test]
        public void PathsToModels_ReturnsListOfFileModels()
        {
        
            // Arrange
            string[] files = { "file1.txt", "file2.txt", "file3.txt" };

            // Act
            List<FileModel> result = _filesToModelsMapper.PathsToModels(files);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<FileModel>>(result);
            Assert.AreEqual(files.Length, result.Count);

            // Additional assertions to check individual file properties
            for (int i = 0; i < files.Length; i++)
            {
                Assert.AreEqual(files[i], result[i].FullPathOfFile);
            }
        }
    
        [Test]
        public void GetFilesAndMapToModels_ValidFiles_ReturnsMappedModels()
        {
            // Arrange
            IIoManagement ioManagement = new IoManagement();
    
            // Get files using IoManagement
            var files = ioManagement.GetFiles(_fileModelTestsPath, true);

            IFilesToModelsMapper filesToModelsMapper = new GenerateFiles();
    
            // Map file paths to file models
            var results = filesToModelsMapper.PathsToModels(files.ToArray());
    
            // Act
            //List<FileModel> result = _filesToModelsMapper.PathsToModels(files);

            // Assert
            Assert.NotNull(files);
            Assert.NotNull(results);
    
            Assert.IsInstanceOf<List<FileModel>>(results);
            Assert.AreEqual(files.Count, results.Count);
    
            // Additional assertions to check individual file properties
            foreach (var fileModel in results)
            {
                var fileName = Path.GetFileName(fileModel.FullPathOfFile);
                Assert.AreEqual(fileName, fileModel.FileName);

                // Check if the DateCreation is not older than 24 hours (1 days)
                var isDateValid = fileModel.DateCreation > DateTime.Now.AddHours(-24);
                Assert.IsTrue(isDateValid, "Date is older than 1 days");
            }    
        }

    }
}