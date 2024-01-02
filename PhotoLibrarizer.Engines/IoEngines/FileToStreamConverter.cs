namespace PhotoLibrarizer.Engines.IoEngines
{
    public class FileToStreamConverter:IFileToStreamConverter
    {
        public Stream? TryConvertFileToStream(string filePath, out string errorMessage)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            errorMessage = "File path cannot be empty.";
            return null;
        }
        
        if (!File.Exists(filePath))
        {
            errorMessage = "File not found.";
            return null;
        }


        FileStream fileStream = File.OpenRead(filePath);
        MemoryStream memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        fileStream.Close();
        
        // Reset the memory stream position to the beginning
        memoryStream.Seek(0, SeekOrigin.Begin);

        errorMessage = string.Empty;
        return memoryStream;
    }
    }
}