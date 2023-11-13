namespace PhotoLibrarizer.Engines.Hashing;

public interface IFileHasher
{
    public string GetMD5ByFilePath(string path);
}