using PhotoLibrarizer.BusinessLogic.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering
{
    public interface IOrderAndCreateLibraryOfMediaBusinessLogicService
    {
        public List<string> GetFilesInPath(FilterBusinessLogicModel filterBusinessLogicModel);
    
        public Task<List<string>> OrderRoutine(FilterBusinessLogicModel filterBusinessLogicModel);
    
        public List<string> GetPresentExtensions();
    }

    public class StoreResults
    {
        public List<string> ResultRawAllFiles { get; set; }
        public List<string> FileExtensions { get; set; }
        /*public List<string> Directories { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Messages { get; set; }
        public List<string> All { get; set; }
        public List<string> AllFiles { get; set; }
        public List<string> AllDirectories { get; set; }
        public List<string> AllErrors { get; set; }
        public List<string> AllWarnings { get; set; }
        public List<string> AllMessages { get; set; }
        public List<string> AllAll { get; set; }
        public List<string> AllAllFiles { get; set; }
        public List<string> AllAllDirectories { get; set; }
        public List<string> AllAllErrors { get; set; }
        public List<string> AllAllWarnings { get; set; }
        public List<string> AllAllMessages { get; set; }
        public List<string> AllAllAll { get; set; }
        public List<string> AllAllAllFiles { get; set; }
        public List<string> AllAllAllDirectories { get; set; }
        public List<string> AllAllAllErrors { get; set; }
        public List<string> AllAllAllWarnings { get; set; }
        public List<string> AllAllAll*/
    }
}