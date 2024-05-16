namespace PhotoLibrarizer.Engines.SSH;

public interface ISshServices
{
    
    
    public string ExecuteCommand(string command, out string error);
    
    public string? GetCorrectNameOfFile(string possibleRemoveNameOfFile, out string error);

    public string? GetCorrectNameOfDirectory(string possibleRemoveNameOfFile, out string error);
}