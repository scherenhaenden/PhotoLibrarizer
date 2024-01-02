namespace PhotoLibrarizer.BusinessLogic.Services.IOService
{
    public class MoveFilesSecure: IMoveFilesSecure
    {
        // Message if source and destination are the same
        const string SameSourceAndDestinationMessage = "Source and destination are the same";
    
        // Message if the file move failed
        const string FileMoveFailedMessage = "File move failed";
    
        // Message if the file move was successful but the source file was not deleted
        const string FileMoveSuccessfulButSourceNotDeletedMessage = "File move successful but source not deleted";
    
    
        public async Task<ResponseModel<bool>> MoveFileAsync(string sourcePath, string destinationPath)
    {

        await Task.Delay(1); // Simulate some work
        if(sourcePath == destinationPath)
        {
            return new ResponseModel<bool>
            {
                Success = true,
                Message = SameSourceAndDestinationMessage
            };
        }

        try
        {
            File.Copy(sourcePath, destinationPath);

        }
        catch (Exception e)
        {
            return new ResponseModel<bool>
            {
                Success = false,
                Message = FileMoveFailedMessage,
                Exception = e
            };
        }
        
        
        var sourceFileInfo = new FileInfo(sourcePath);
        var destinationFileInfo = new FileInfo(destinationPath);
        
        if (sourceFileInfo.Length == destinationFileInfo.Length)
        {
            try
            {
                File.Delete(sourcePath);
                return new ResponseModel<bool>
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = FileMoveSuccessfulButSourceNotDeletedMessage,
                    Exception = e
                };
            }
           
        }

        return new ResponseModel<bool>
        {
            Success = false,
            Message = FileMoveSuccessfulButSourceNotDeletedMessage
        };

    }
    }
}