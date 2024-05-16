using PhotoLibrarizerCore.Services.NamingPictures;

namespace PhotoLibrarizerCore.Tests.Services.NamingPictures
{
    [TestFixture]
    public class PicturesNamerServiceV1Tests
    {
        private PicturesNamerServiceV1 picturesNamerService;

        [SetUp]
        public void Setup()
    {
        picturesNamerService = new PicturesNamerServiceV1();
    }

        [Test]
        public void GenerateName_ValidModel_ReturnsExpectedName()
    {
        // Arrange
        var model = new NamerModel
        {
            Dates = new List<DatesModel>
            {
                new DatesModel { Date = new DateTime(2023, 5, 1, 10, 30, 0) },
                new DatesModel { Date = new DateTime(2023, 5, 2, 12, 0, 0) }
            },
            FileSize = 1024,
            Hashtag = "nature"
        };

        // Act
        string result = picturesNamerService.GenerateName(model);

        // Assert
        Assert.AreEqual("2023_05_01_10_30_00_size_1024_hashtag_nature", result);
    }

        [Test]
        public void GenerateName_NullModel_ThrowsArgumentNullException()
    {
        // Arrange
        NamerModel model = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => picturesNamerService.GenerateName(model));
    }

        [Test]
        public void GenerateName_EmptyDates_ThrowsArgumentException()
    {
        // Arrange
        var model = new NamerModel
        {
            Dates = new List<DatesModel>(),
            FileSize = 1024,
            Hashtag = "nature"
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => picturesNamerService.GenerateName(model), "Dates cannot be empty");
    }
    }
}