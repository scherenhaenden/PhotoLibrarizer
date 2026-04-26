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
            
            //new way
            Assert.That(result, Is.Not.Null);
            //new way
            Assert.That(result, Is.InstanceOf<List<FileModel>>());
            //new way
            Assert.That(files.Length, Is.EqualTo(result.Count));
            

            // Additional assertions to check individual file properties
            for (int i = 0; i < files.Length; i++)
            {
                // new way
                Assert.That(files[i], Is.EqualTo(result[i].FullPathOfFile));
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
            
            // new way
            Assert.That(files, Is.Not.Null);
            // new way
            Assert.That(results, Is.Not.Null);
    
            // new way
            Assert.That(files, Is.InstanceOf<List<string>>());
            // new way
            Assert.That(files.Count, Is.EqualTo(results.Count));
            
    
            // Additional assertions to check individual file properties
            foreach (var fileModel in results)
            {
                var fileName = Path.GetFileName(fileModel.FullPathOfFile);
                // new way
                Assert.That(fileName, Is.EqualTo(fileModel.FileName));

                // Check if the DateCreation is not older than 24 hours (1 days)
                var isDateValid = fileModel.DateCreation > DateTime.Now.AddHours(-24);
                // new way for Assert.IsTrue(isDateValid, "Date is older than 1 days");
                Assert.That(isDateValid, Is.True, "Date is older than 1 days");
                
            }    
        }

    }
}