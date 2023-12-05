namespace PhotoLibrarizer.Engines.IoEngines;

public static class StreamHelper
{
    public static void ToFile(this Stream stream, string filePath)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));

        using (FileStream fileStream = File.Create(filePath))
        {
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
        }
    }
}