using Renci.SshNet;

namespace PhotoLibrarizer.Engines.SSH;

public class SshServices:ISshServices
{
    
    private readonly string _host;
    private readonly string _username;
    private readonly string _password;
    private static SshClient? _sshClient;

    public SshServices(string host, string username, string password)
    {
        _host = host;
        _username = username;
        _password = password;
       
    }
    
    // check if connected
    private static bool IsConnected()
    {
        return _sshClient?.IsConnected ?? false;
    } 
    
    // connect
    static object _lockConnection = new object();
    static object _lock2 = new object();
    private static void Connect(string host, string username, string password)
    {
        lock (_lockConnection)
        {
            
            if (!IsConnected())
            {
                _sshClient = new SshClient(host, username, password);
                _sshClient.Connect();
                // cd to "/volume1/Edward"Ï
                _sshClient.CreateCommand("cd /volume1/Edward").Execute();
            }
        }
        
        
    }
    
    

    public async Task<ISshServices.SshResponseModel> ExecuteCommandAsync(string command)
    {
        try
        {
           
            
            
            
                
                //IsConnected() ? false ? _sshClient.Connect() : _sshClient.Disconnect();
                Connect(_host, _username, _password);
                //var result = _sshClient.RunCommand(command);

                var sshCommand = _sshClient.CreateCommand(command);
                await sshCommand.ExecuteAsync();
                //var result = await sshCommand.ExecuteAsync();
                var result = sshCommand.Result;
                return new ISshServices.SshResponseModel
                {
                    Response = result,
                    Error = sshCommand.Error
                };
                
            
            
           

           
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ISshServices.SshResponseModel
            {
                Response = e.Message,
                Error = e.Message
            };
        }

    

    }

    public string ExecuteCommand(string command, out string error)
    {
        try
        {
            lock (_lock2)
            { 
                
                //IsConnected() ? false ? _sshClient.Connect() : _sshClient.Disconnect();
                Connect(_host, _username, _password);
                //var result = _sshClient.RunCommand(command);
                _sshClient.CreateCommand("cd /volume1/Edward").Execute();
                var sshCommand = _sshClient.CreateCommand(command);
                var result = sshCommand.Execute();
                if (!string.IsNullOrEmpty(sshCommand.Error))
                {
                    Console.WriteLine(sshCommand.Error);
                    error = sshCommand.Error;
                    return sshCommand.Error;
                }
                error = string.Empty;
                return result;
                
            }
            
           

           
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            error = e.Message;
            return e.Message;
        }
        

    }

    public string? GetCorrectNameOfFile(string possibleRemoveNameOfFile, out string error)
    {
        
        var command = $"ls '{possibleRemoveNameOfFile}'";
        var result = ExecuteCommand(command, out error);
        var pwd = ExecuteCommand("pwd", out error);
        
        if (string.IsNullOrEmpty(error))
        {
            //result = command.Substring(3).Replace("\n", "");
            return possibleRemoveNameOfFile;
        }
        
        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls '{possibleRemoveNameOfFile}'";
            result = ExecuteCommand(command, out error);
            // replace the first 3 letters of command
            result = command.Substring(3).Replace("\n", ""); //result.Replace(command.Substring(0, 3), "");
        }
                
        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls {possibleRemoveNameOfFile.Replace(" ", "\\ ")}";
            result = ExecuteCommand(command, out error);
            // replace the first 3 letters of command
            result = result.Replace(command.Substring(0, 3), "");
        }

        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls {possibleRemoveNameOfFile.Replace(" ", "*")}";
            result = ExecuteCommand(command, out error);
        }
      
        return string.IsNullOrEmpty(error) ? result.Replace("\n", "") : null;
    }

    public static string ReplacePathSegment(string originalPath, string pivotDirectory, string replacementPath,
        out string error)
    {
        error = string.Empty;

        // Ensure the pivot directory starts with a '/'
        if (!pivotDirectory.StartsWith("/"))
        {
            pivotDirectory = "/" + pivotDirectory;
        }

        // Find the index of the pivot directory in the original path
        int pivotIndex = originalPath.IndexOf(pivotDirectory, StringComparison.Ordinal);
        if (pivotIndex == -1)
        {
            error = "Pivot directory not found in the original path.";
            return string.Empty;
        }

        // Extract the remaining path after the pivot directory
        string remainingPath = originalPath.Substring(pivotIndex + pivotDirectory.Length);

        // Create the new path by combining the replacement path with the remaining part
        string newPath = replacementPath + remainingPath;
        return newPath;
    }





    public string? GetCorrectNameOfFileUsingItsPath(string filePathIo, out string error)
    {
        // get full directory from filePathIo
        var directory = Path.GetDirectoryName(filePathIo);
        
        // create command to obtain current pwd
        var command = $"pwd";
        var pwd = ExecuteCommand(command, out error);
        
        pwd = pwd.Replace("\n", "");
        
        // this should be only temporary
        string pivotDirectory = "/Edward";
        
        
        string newPath = ReplacePathSegment(filePathIo, pivotDirectory, pwd, out error);
        return newPath;

    }

    

    public string? GetCorrectNameOfDirectory(string possibleRemoveNameOfFile, out string error)
    {
        var command = $"ls -d '{possibleRemoveNameOfFile}'";
        var result = ExecuteCommand(command, out error);
        
        if (string.IsNullOrEmpty(error))
        {
            return possibleRemoveNameOfFile;
        }
                
        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls -d {possibleRemoveNameOfFile.Replace(" ", "\\ ")}";
            result = ExecuteCommand(command, out error);
        }

        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls -d {possibleRemoveNameOfFile.Replace(" ", "*")}";
            result = ExecuteCommand(command, out error);
        }
      
        return string.IsNullOrEmpty(error) ? result.Replace("\n", "") : null;
    }

    public string ReplacementForSshPathVolumes(string path, string search, string replaceWith)
    {
        if (path.StartsWith(search))
        {
            // Remove the starting "/Volumes" and prepend with "/volume1"
            return replaceWith + path.Substring(search.Length);
        }
        return path;
    }
    
    
    // write destructor
    ~SshServices()
    {
        _sshClient?.Disconnect();
    }
}