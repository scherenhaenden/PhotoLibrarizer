using PhotoLibrarizerCore.Services.FilesManagement;

namespace PhotoLibrarizerCore.Routines
{
    public interface IOrderMediaFiles
    {
        public void Do(string pathOrigin, string pathDestiny);
    }

    public class OrderMediaFiles: IOrderMediaFiles
    {
        public void Do(string pathOrigin, string pathDestiny)
    {
        // Get files
        // Get files into models
        // Order files
    }
    }



    public interface IRenameFilesToFormat
    {
        public void Do(string pathOrigin, string pathDestiny, string[]? extensions, bool recursive = false);
    }

    public class RenameFilesToFormat : IRenameFilesToFormat
    {
        private readonly IIoManagement _ioManagement;

        public RenameFilesToFormat(IIoManagement ioManagement)
    {
        _ioManagement = ioManagement;
    }
    
        public void Do(string pathOrigin, string pathDestiny, string[]? extensions, bool recursive = false)
    {
        //Get files
        var files = _ioManagement.GetFiles(pathOrigin, extensions, recursive);
        
        GenerateFiles generateFiles = new GenerateFiles();

        //Get files into models
        var generatedFileModels = generateFiles.PathsToModels(files.ToArray());
    }
    }
}