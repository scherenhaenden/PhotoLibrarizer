using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.Routines.SimpleRoutines
{
    public interface IFilterFilesModels
    {
        public List<FileModel> Filter(FilterModel filterModel, List<FileModel> fileModels);
    }
}