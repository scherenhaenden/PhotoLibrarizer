namespace PhotoLibrarizer.Engines.Models.DateModels
{
    public interface IReadFileInfo
    {
        DateTime? GetDateOfMediaCreation(string filePath);

        string GetFileSizeHumanReadable(string filePath);

        string GetFileSize(string filePath);
    }
}