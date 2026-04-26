using PhotoLibrarizer.BusinessLogic.Services.FileSeeking;
using PhotoLibrarizer.Engines.Hashing;
using PhotoLibrarizer.Engines.IoEngines.FilesModelsMapper;
using PhotoLibrarizer.Engines.IoEngines.Seekers;
using PhotoLibrarizer.Engines.Metadata;

namespace PhotoLibrarizer.BusinessLogic.Test.Composed.FileRenamings;

public class FileRenamingTests
{
    [Test]
    public void RenameFilesInDirectory ()
    {
        var path = "/Volumes/Extern/created_on_linux/Photos/2024.08.03/DOALL";
        IFilesSeekerV2 filesSeekerV2 = new FilesSeekerV2();
        
        List<string> extensions = new List<string> { ".jpg", ".jpeg", ".png" };
        
        List<string> files = filesSeekerV2
            .GetFilesInPath(path, extensions, false, false)
            .Where(x => !x.ToLower().Contains("Screen".ToLower())).ToList();
        
        IFileModelsMapper fileModelsMapper = new FileModelsMapper();

        var fileModels = fileModelsMapper.MapPaths(files) /*.Take(filterBusinessLogicModel.MaxFiles ?? 10000)*/
            .ToList();


        foreach (var fileModel in fileModels)
        {
            IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
            var currentDateOfFile = metadataManager.GetDateOfMediaCreation();
            fileModel.Directories = metadataManager.Directories;
            
      
            
            // create new name of file
            // 1.- get name of file with extension
            
            var dateForName = currentDateOfFile?.ToString("yyyy_MM_dd_HH_mm_ss");
            // get size
            var size = fileModel.Size;
            IFileHasher fileHasher = new FileHasherDotnet();
            var hash = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
            
            var newName = dateForName + "_" + size + "_" + hash + Path.GetExtension(fileModel.FullPathOfFile);
           
           
            
            var newPath = Path.Combine(Path.GetDirectoryName(fileModel.FullPathOfFile) ?? string.Empty, newName);
            
            if (File.Exists(newPath))
            {
                Console.WriteLine("file already exists");
            }
            else
            {
                File.Move(fileModel.FullPathOfFile, newPath);
                if (File.Exists(newPath))
                {
                    Console.WriteLine("file moved");
                    //File.Delete(fileModel.FullPathOfFile);
                }
                else
                {
                    Console.WriteLine("file not moved");
                }
            }
            
            
        }
        
    
    }
}