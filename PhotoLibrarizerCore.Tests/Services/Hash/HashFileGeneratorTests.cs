using System.Security.Cryptography;
using System.Text;
using Moq;
using PhotoLibrarizerCore.Services.Hash;
using PhotoLibrarizerCore.Tests.Tests_Helper;

namespace PhotoLibrarizerCore.Tests.Services.Hash
{
    [TestFixture]
    public class HashFileGeneratorTests
    {
        private HashFileGenerator hashFileGenerator;
    

        [SetUp]
        public void Setup()
        {
            hashFileGenerator = new HashFileGenerator();
            var currentDirectory = TestsHelper.GetTestsPath();
        }

        [Test]
        public void GetHashByFilePath_ValidFilePath_ReturnsHash()
        {
            // Arrange
            string filePath = "path/to/file.txt";
            string expectedHash = "expected-hash";

            using (var stream = new MemoryStream())
            {
                // Create a file with some content
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("file content");
                    writer.Flush();
                    stream.Position = 0;

                    // Mock the File.OpenRead method to return the stream
                    var fileMock = new Mock<FileStream>();
                    fileMock.Setup(f => f.Name).Returns(filePath);
                    fileMock.Setup(f => f.Length).Returns(stream.Length);
                    fileMock.Setup(f => f.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                        .Callback<byte[], int, int>((buffer, offset, count) =>
                        {
                            stream.Read(buffer, offset, count);
                        })
                        .Returns((byte[] buffer, int offset, int count) =>
                        {
                            return count;
                        });

                    // Mock the MD5.Create method to return a mock of MD5
                    var md5Mock = new Mock<MD5>();
                    md5Mock.Setup(m => m.ComputeHash(It.IsAny<byte[]>()))
                        .Returns(Encoding.UTF8.GetBytes(expectedHash));

                    // Inject the mocks into the HashFileGenerator instance
                    //hashFileGenerator.FileOpener = (path, mode, access) => fileMock.Object;
                    //hashFileGenerator.MD5Factory = () => md5Mock.Object;

                    // Act
                    string result = hashFileGenerator.GetHashByFilePath(filePath);

                    // Assert
                    Assert.AreEqual(expectedHash, result);
                }
            }
        }
    }
}