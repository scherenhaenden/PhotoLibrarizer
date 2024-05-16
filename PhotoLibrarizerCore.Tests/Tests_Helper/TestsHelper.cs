namespace PhotoLibrarizerCore.Tests.Tests_Helper
{
    public class TestsHelper
    {
        public static string GetTestsPath()
    {
        // Get current directory
        var currentDirectory = Directory.GetCurrentDirectory();
        
        // Get up 3 directories
        currentDirectory = Path.Combine(currentDirectory, "..","..", "..");
        
        // Combine with test folder DataforTests only
        return Path.Combine(currentDirectory, "DataForTests");

    }
    }
}