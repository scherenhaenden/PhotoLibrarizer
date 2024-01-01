namespace PhotoLibrarizer.BusinessLogic.Services.IOService;

public interface IMoveFilesSecure
{
    public Task<ResponseModel<bool>> MoveFileAsync(string sourcePath, string destinationPath);
}