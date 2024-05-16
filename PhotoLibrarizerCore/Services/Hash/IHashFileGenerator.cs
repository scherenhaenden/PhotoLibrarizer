namespace PhotoLibrarizerCore.Services.Hash
{
    public interface IHashFileGenerator
    {
        string GetHashByFilePath(string path);
    }
}
