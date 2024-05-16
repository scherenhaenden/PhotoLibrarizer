using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.Engines.IoEngines.FilesModelsMapper;

public interface IFileModelsGetterBySeekerGenerator
{
    
}


public class FileModelsGetterBySeekerGenerator: IFileModelsGetterBySeekerGenerator
{
    private readonly FilesSeekerV2 _filesSeeker;

    public FileModelsGetterBySeekerGenerator(FilesSeekerV2 filesSeeker)
    {
        _filesSeeker = filesSeeker;
    }
    
    
    public List<FileModel> SeekfileAndGenerateFileModels(string path, bool subDirectories = true)
    {
        var files = _filesSeeker.GetFilesInPath(path, subDirectories);
        // 1.1 - translate path to models
        GenerateFiles generateFiles = new GenerateFiles();
        var fileModels = generateFiles.StringsToModels(files.ToArray());
        return fileModels;
    }

}

public class FileModelsMapper: IFileModelsMapper
{
    public List<FileModel> MapPaths(List<string> paths)
    {
        GenerateFiles generateFiles = new GenerateFiles();
        var fileModels = generateFiles.StringsToModels(paths.ToArray());
        return fileModels;
    }
}