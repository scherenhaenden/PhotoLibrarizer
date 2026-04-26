using Microsoft.EntityFrameworkCore;
using PhotoLibrarizer.Engines.Database;

namespace PhotoLibrarizer.Engines.Tests.Implementations.Database;

public class PhotoDatabaseContextTests
{
    [Test]
    public void TestMethod1()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PhotoDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "PhotoDatabase")
            .Options;

        // Act
        using (var context = new PhotoDatabaseContext(options))
        {
            // New way of assert
            Assert.That(context, Is.Not.Null);
        }
    }
}