namespace PhotoLibrarizer.BusinessLogic.Services.FileSeeking
{
    public interface IFileSeekerBusinessLogicService
    {
        public List<string> GetFilesInPath(List<string> paths, bool subDirectory = true);
    }
}