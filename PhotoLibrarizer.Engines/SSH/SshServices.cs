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
    private static void Connect(string host, string username, string password)
    {
        if (!IsConnected())
        {
            _sshClient = new SshClient(host, username, password);
            _sshClient.Connect();
        }
    }
    
    static object _lock = new object();
    
    public string ExecuteCommand(string command, out string error)
    {
        try
        {
            lock (_lock)
            { 
                
                //IsConnected() ? false ? _sshClient.Connect() : _sshClient.Disconnect();
                Connect(_host, _username, _password);
                //var result = _sshClient.RunCommand(command);

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
        var command = $"ls {possibleRemoveNameOfFile}";
        var result = ExecuteCommand(command, out error);
                
        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls {possibleRemoveNameOfFile.Replace(" ", "\\ ")}";
            result = ExecuteCommand(command, out error);
        }

        if (!string.IsNullOrEmpty(error))
        {
            command = $"ls {possibleRemoveNameOfFile.Replace(" ", "*")}";
            result = ExecuteCommand(command, out error);
        }
      
        return string.IsNullOrEmpty(error) ? result.Replace("\n", "") : null;
    }
    
    public string? GetCorrectNameOfDirectory(string possibleRemoveNameOfFile, out string error)
    {
        var command = $"ls -d {possibleRemoveNameOfFile}";
        var result = ExecuteCommand(command, out error);
                
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