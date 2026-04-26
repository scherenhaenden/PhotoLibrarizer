using PhotoLibrarizer.Engines.Hashing;
using PhotoLibrarizer.Engines.Models;
using PhotoLibrarizer.Engines.Models.DateModels;

namespace PhotoLibrarizer.BusinessLogic.Services.NamingOfFiles;

public interface IFilesRenamer
{
    public string RenameFileAndGetNewName(FileModel fileModel, DateTime dateOfCreation);

}

public class FilesRenamer: IFilesRenamer
{
    public string RenameFileAndGetNewName(FileModel fileModel, DateTime dateOfCreation)
    {
        IReadFileInfo readFileInfo = new ReadFileInfo();
        var newName=dateOfCreation.ToString("yyyy_MM_dd_HH_mm_ss");
        var newNameFullname=dateOfCreation.ToString("yyyy_MM_dd_HH_mm_ss");

        var filename=fileModel.FileName;

        var directoryname=fileModel.PathOfFile;

        var extension=Path.GetExtension(fileModel.FullPathOfFile).ToLower();
            
        var size=readFileInfo.GetFileSize(fileModel.FullPathOfFile);
        var  maybeOfficialName=directoryname+"/"+newName+"_"+size+extension;
        
        // check if new name and old name are the same
        if (fileModel.FullPathOfFile==maybeOfficialName) {
        
         return maybeOfficialName;
        }     
        // 4.- try rename files with datetime
        // 5.- if rename fails, try rename with datetime and size
        // actually doing it right away
        // check if file exists
        if (File.Exists(maybeOfficialName))
        {
            // add hash of file to name
            // 6.- if rename fails, try rename with datetime and size and hash
            IFileHasher fileHasher = new FileHasherDotnet();
            var hash = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
            maybeOfficialName=directoryname+"/"+newName+"_"+size+"_"+hash+extension;
              
                
            if (File.Exists(maybeOfficialName))
            {
                // add size of file to name
                //Size=readFileInfo.GetFileSize(fileModel.FullPathOfFile);
                return maybeOfficialName;
            }
        
            
             File.Move(fileModel.FullPathOfFile, maybeOfficialName);
            }             
                
      
        return maybeOfficialName;
        
    }
}