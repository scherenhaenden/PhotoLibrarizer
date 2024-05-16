namespace PhotoLibrarizer.BusinessLogic.Services.IOService;


public interface IFileComparer
{
    bool FileExists(string path);
    
    bool DeleteIfFileExists(string path);
    
    bool DeleteIfFileExistsComparingByHash(string path);
}


/*
public interface IFileExistenceHandler
{
    bool FileExists(string path);
    
    bool DeleteIfFileExists(string path);
    
    bool DeleteIfFileExistsComparingByHash(string path);
}

public class FileExistenceHandler: IFileExistenceHandler
{
    /*private readonly IFileHashHandler _fileHashHandler;
    
    public FileExistenceHandler(IFileHashHandler fileHashHandler)
    {
        _fileHashHandler = fileHashHandler;
    }
    
    public bool FileExists(string path)
    {
        return File.Exists(path);
    }* /
    
    public bool DeleteIfFileExists(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        return false;
    }
    
    public bool DeleteIfFileExistsComparingByHash(string path)
    {
        if (File.Exists(path))
        {
            var hash = _fileHashHandler.GetFileHash(path);
            if (hash == null)
            {
                return false;
            }
            var hashPath = Path.Combine(Path.GetDirectoryName(path) ?? throw new InvalidOperationException(), hash);
            if (File.Exists(hashPath))
            {
                File.Delete(path);
                return true;
            }
        }
        return false;
    }
}*/