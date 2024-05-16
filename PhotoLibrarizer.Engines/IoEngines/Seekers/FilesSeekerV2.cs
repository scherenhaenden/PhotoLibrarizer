namespace PhotoLibrarizer.Engines.IoEngines
{
    public class FilesSeekerV2: IFilesSeekerV2
    {

        public List<string> GetFilesInPath(string path, bool subDirectory = true)
    {
        List<string> files = Directory
            .GetFiles(path, "*.*", subDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            
            .ToList();

        return files;
    }

        public List<string> GetFilesInPath(string path, List<string> extensions, bool subDirectory = true, bool caseSensitive = false)
    {
        var files = GetFilesInPath(path, subDirectory);
        StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        files = files.Where(file => extensions.Any(extension => file.EndsWith(extension, comparison))).ToList();

        return files;
    }

        public List<string> GetFilesInPath(List<string> path, bool subDirectory = true)
    {
        return path
            .SelectMany(path => GetFilesInPath(path,  subDirectory))
            .ToList();
    }

        public List<string> GetFilesInPath(List<string> path, List<string>? extensions, bool subDirectory = true, bool caseSensitive = false)
    {
        if(extensions == null)
            return GetFilesInPath(path, subDirectory);
        
        
        return path
            .SelectMany(path => GetFilesInPath(path, extensions, subDirectory, caseSensitive))
            .ToList();
    }
    }
}