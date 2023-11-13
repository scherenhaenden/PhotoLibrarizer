namespace PhotoLibrarizer.Engines.IoEngines;

public interface IFileToStreamConverter
{
    Stream? TryConvertFileToStream(string filePath, out string errorMessage);
}