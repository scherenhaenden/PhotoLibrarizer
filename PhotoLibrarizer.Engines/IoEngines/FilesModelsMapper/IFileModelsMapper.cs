using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.Engines.IoEngines.FilesModelsMapper;

public interface IFileModelsMapper
{
    public List<FileModel> MapPaths(List<string> paths);
    
}