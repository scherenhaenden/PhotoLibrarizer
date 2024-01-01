namespace PhotoLibrarizer.BusinessLogic.Services.IOService;

public class MoveFiles: IMoveFiles
{
    
    
    public async Task MoveFileAsync(string sourcePath, string destinationPath)
    {
        //Console.WriteLine($"Moving file: {sourcePath} to {destinationPath}");
        await Task.Delay(1); // Simulate some work
        
        
        if(sourcePath == destinationPath)
        {
            return;
        }
        
        File.Copy(sourcePath, destinationPath);
        
        // check if both files are the same size
        var sourceFileInfo = new FileInfo(sourcePath);
        var destinationFileInfo = new FileInfo(destinationPath);
        
        if (sourceFileInfo.Length == destinationFileInfo.Length)
        {
            File.Delete(sourcePath);
        }
        else
        {
            //Console.WriteLine($"File move failed: {sourcePath} to {destinationPath}");
        }
    }
}