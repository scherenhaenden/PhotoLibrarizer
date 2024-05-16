using PhotoLibrarizer.Web.Blazor.Models;

namespace PhotoLibrarizer.Web.Blazor.Data
{
    public interface IOrderAndCreateLibraryOfMediaWebService
    {
        public List<string> GetFilesInPath(FilterWebModel filterBusinessLogicModel);
    
        public Task<List<string>> OrderRoutine(FilterWebModel filterBusinessLogicModel);
        public List<string> GetPresentExtensions();
    
    }
}