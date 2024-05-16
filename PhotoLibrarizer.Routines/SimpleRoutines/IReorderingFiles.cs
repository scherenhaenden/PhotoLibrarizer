using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.Routines.SimpleRoutines
{
    // Define a delegate for the output
    public delegate void OutputDelegate(string message);

    public interface IReorderingFiles
    {
        public Task<string> DoRenameFilesAsync_SimpleDraft(string path, bool subDirectory = true, bool movesToBaseLibrary = false);

        public Task DoRenameFilesAsync(string path, string pattern, string replacement, bool recursive, bool dryRun);

        public Task DoByFileModelAsync(FilterModel filterModel);
    }

    public interface IReorderingFilesFindByProperties
    {
        public Task<string> DoRenameFilesAsync_SimpleDraft(string path, bool subDirectory = true, bool movesToBaseLibrary = false);

        public Task DoRenameFilesAsync(string path, string pattern, string replacement, bool recursive, bool dryRun);
    
        public Task DoRenameFilesAsync(string path, FilterModel filterModel, bool cameraBasedDirectory,string pattern, bool including = true);

        public Task AllDoingsHereBasedOnDirectory(bool movesToBaseLibrary, List<FileModel> fileModels,
            bool renaming = true);
    }
}

