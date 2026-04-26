namespace PhotoLibrarizer.Engines.SSH;

public interface ISshServices
{

    public class SshResponseModel
    {
        public string Response { get; set; }
        public string Error { get; set; }
    }
    public Task<SshResponseModel> ExecuteCommandAsync(string command);
    public string ExecuteCommand(string command, out string error);
    
    public string? GetCorrectNameOfFile(string possibleRemoveNameOfFile, out string error);
    
    public string? GetCorrectNameOfFileUsingItsPath(string filePathIo, out string error);

    public string? GetCorrectNameOfDirectory(string possibleRemoveNameOfFile, out string error);
}