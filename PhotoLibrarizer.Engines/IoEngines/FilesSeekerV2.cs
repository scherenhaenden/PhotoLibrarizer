namespace PhotoLibrarizer.Engines.IoEngines;

public class FilesSeekerV2: IFilesSeekerV2
{

    public List<string> GetFilesInPath(string path, bool subDirectory = true)
    {
        List<string> files = Directory
            .GetFiles(path, "*.*", subDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            //.Where(file => allowedExtensions.Any(extension => file.EndsWith(extension, comparison)))
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

}