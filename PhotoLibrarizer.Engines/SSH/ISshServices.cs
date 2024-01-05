using Renci.SshNet;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering
{
    public interface ISshServices
    {
        public string ExecuteCommand(string command, out string error);
    
        public string? GetCorrectNameOfFile(string possibleRemoveNameOfFile, out string error);

        public string? GetCorrectNameOfDirectory(string possibleRemoveNameOfFile, out string error);
    }

    public class SshServices:ISshServices
    {
        private readonly SshClient _sshClient;

        public SshServices(string host, string username, string password)
        {
            _sshClient = new SshClient(host, username, password);
        }
    
        // check if connected
        private bool IsConnected()
        {
            return _sshClient.IsConnected;
        } 
    
        // connect
        private void Connect()
        {
            if (!IsConnected())
            {
                _sshClient.Connect();
            }
        }
    
        public string ExecuteCommand(string command, out string error)
        {
            //IsConnected() ? false ? _sshClient.Connect() : _sshClient.Disconnect();
            Connect();
            var result = _sshClient.RunCommand(command);
            //_sshClient.Disconnect();
            error = result.Error;
            return result.Result;
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
            _sshClient.Disconnect();
        }
    }
}