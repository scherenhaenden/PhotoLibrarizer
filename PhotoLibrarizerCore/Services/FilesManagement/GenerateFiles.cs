using PhotoLibrarizerCore.Services.FilesManagement.Models;
using PhotoLibrarizerCore.Services.Hash;

namespace PhotoLibrarizerCore.Services.FilesManagement
{
    public interface IFilesToModelsMapper
    {
        List<FileModel> PathsToModels(string [] Files);
    }

    public class GenerateFiles: IFilesToModelsMapper
    {
        public List<FileModel> DeleteDuplicatesOfList(List<FileModel> allFiles, IHashFileGenerator hashFileGenerator)
        {
            return new FileModelDeleteDuplicates(hashFileGenerator).Run(allFiles);            
        }
    
        public List<FileModel> GenerateFilesList_(List<string> Files)
        {
            GenerateFiles gf = new GenerateFiles ();
            var allFiles=gf.PathsToModels (Files.ToArray());
            return allFiles;
        }

        public List<FileModel> PathsToModels(string [] Files)
        {
            List<FileModel> AllFiles = new List<FileModel> ();
            FileModel TempFile;
            foreach(var filestring in Files)
            {
				    
                TempFile = new FileModel ();
                TempFile.FullPathOfFile = filestring;
                AllFiles.Add (TempFile);
            }
            return AllFiles;
        }

    }
}