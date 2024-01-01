using PhotoLibrarizer.Engines.IoEngines;

namespace PhotoLibrarizer.BusinessLogic.Services.FileSeeking
{
    public interface IFileSeekerBusinessLogicService
    {
        public List<string> GetFilesInPath(List<string> paths, bool subDirectory = true);
    }

    public class FileSeekerBusinessLogicService: IFileSeekerBusinessLogicService
    {
        IFilesSeekerV2 _filesSeekerV2;
    
        public FileSeekerBusinessLogicService()
    {
        _filesSeekerV2 = new FilesSeekerV2();
    }
    
    
        public List<string> GetFilesInPath(List<string> paths, bool subDirectory = true)
    {
        return _filesSeekerV2.GetFilesInPath(paths, subDirectory);
    }
    }
}