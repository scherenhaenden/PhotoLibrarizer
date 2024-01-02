namespace PhotoLibrarizerCore.Services.FilesManagement
{
    public interface IIoManagement
    {
        List<string> GetFiles(string path, bool recursive = false);
        List<string> GetFiles(string path, string[]?extensions ,   bool recursive = false);
    
        List<string> GetFiles(string path, string[]?extensions , bool recursive = false, bool knownMediaExtensions = false);
    }
}