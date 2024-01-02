namespace PhotoLibrarizer.BusinessLogic.Services.IOService
{
    public interface IMoveFiles
    {
        public Task MoveFileAsync(string sourcePath, string destinationPath);
    }
}